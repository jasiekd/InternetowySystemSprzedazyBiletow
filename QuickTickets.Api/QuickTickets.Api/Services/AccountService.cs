using System.Security.Cryptography;
using System.Text;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;
        public AccountService(ITokenService tokenService, DataContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }
        private bool AccountEntityExists(Guid id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool LoginEmailExists(string email, string login)
        {
            return (_context.Accounts?.Any(e => e.Email == email || e.Login == login)).GetValueOrDefault();
        }

        public async Task<IActionResult> Update(RegisterInfoDto registerInfoDto, Guid accountId)
        {
            AccountEntity accountEntity = await _context.Accounts.FindAsync(accountId);
            if (LoginEmailExists(registerInfoDto.Email, registerInfoDto.Login) && (registerInfoDto.Email != accountEntity.Email || registerInfoDto.Login != accountEntity.Login))
            {
                return new BadRequestObjectResult("User istnieje!");
            }

            accountEntity.Surname = registerInfoDto.Surname;
            accountEntity.Name = registerInfoDto.Name;
            accountEntity.Login = registerInfoDto.Login;
            accountEntity.DateOfBirth = registerInfoDto.DateOfBirth;
            accountEntity.Password = HashPassword(registerInfoDto.Password);
            accountEntity.Email = registerInfoDto.Email;

            try
            {
                _context.Accounts.Update(accountEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountEntityExists(accountId))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }
            return new OkResult();
        }

        public async Task<IActionResult> Register(RegisterInfoDto registerInfoDto)
        {
            if (_context.Accounts == null)
            {
                return new NotFoundResult();
            }
            AccountEntity accountEntity = new AccountEntity
            {
                Id = Guid.NewGuid(),
                Name = registerInfoDto.Name,
                Surname = registerInfoDto.Surname,
                Login = registerInfoDto.Login,
                Email = registerInfoDto.Email,
                Password = HashPassword(registerInfoDto.Password),
                DateOfBirth = registerInfoDto.DateOfBirth,
                RoleID = 2
            };

            if (LoginEmailExists(accountEntity.Email, accountEntity.Login))
                return new NotFoundResult();
            _context.Accounts.Add(accountEntity);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> Delete(Guid accountID)
        {
            if (_context.Accounts == null)
            {
                return new NotFoundResult();
            }
            if (!AccountEntityExists(accountID))
            {
                return new NotFoundResult();
            }

            var accountEntity = await _context.Accounts.FindAsync(accountID);

            accountEntity.IsDeleted = true;

            _context.Accounts.Update(accountEntity);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        public async Task<IActionResult> GetListOfUsers(PaginationDto paginationDto)
        {
            if (_context.Accounts == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(await GetAllUsers(paginationDto));
        }
        public async Task<UserInfoDto> GetAccount(Guid accountId)
        {
            AccountEntity accountEntity = await _context.Accounts.FindAsync(accountId);

            UserInfoDto temp = GetUserInfoDto(accountEntity);
            return temp;
        }

        public TokenInfoDto LoginUser(UserLoginRequestDto loginData)
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            else
            {
                var accountEntity = _context.Accounts.Where(x=> x.Login == loginData.UserName && x.IsDeleted==false).FirstOrDefault();
                if (accountEntity!=null) {
                    SHA256 sha256 = SHA256Managed.Create();
                    byte[] bytes = Encoding.UTF8.GetBytes(loginData.Password);
                    byte[] hash = sha256.ComputeHash(bytes);
                    string password = Convert.ToBase64String(hash);
                    if (accountEntity.Password == password) {
                        var result = new TokenInfoDto();
                        result.AccessToken = _tokenService.GenerateBearerToken(accountEntity.Id.ToString(),accountEntity.RoleID.ToString());
                        result.RefreshToken = _tokenService.GenerateRefreshToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<GoogleJsonWebSignature.Payload> GetGooglePayload(string idToken)
        {
            //Will throw a InvalidJwtException if the passed value is not valid JWT
            GoogleJsonWebSignature.Payload validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken);

            return validPayload;
        }

        public bool EmailAlreadyExist(string email)
        {
            return (_context.Accounts?.Any(e => e.Email == email)).GetValueOrDefault();
        }
        public bool SubjectAlreadyExist(string subject)
        {
            return (_context.Accounts?.Any(e => e.GoogleSubject == subject)).GetValueOrDefault();
        }
        public TokenInfoDto LoginUserWithGoogle(GoogleLoginRequestDto loginData)
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            else
            {
                var payload = GetGooglePayload(loginData.Credentials);
                if (SubjectAlreadyExist(payload.Result.Subject))
                {
                    var accountEntity = _context.Accounts.Where(x => x.GoogleSubject == payload.Result.Subject).FirstOrDefault();
                    if (accountEntity.IsDeleted == true)
                    {
                        return null;
                    }
                    var result = new TokenInfoDto();
                    result.AccessToken = _tokenService.GenerateBearerToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());
                    result.RefreshToken = _tokenService.GenerateRefreshToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());

                    return result;
                }
                else
                {
                    if (EmailAlreadyExist(payload.Result.Email))
                    {
                        var accountEntity = _context.Accounts.Where(x => x.Email == payload.Result.Email).FirstOrDefault();
                        if (accountEntity.IsDeleted == true)
                        {
                            return null;
                        }
                        accountEntity.GoogleSubject = payload.Result.Subject;
                        _context.Accounts.Update(accountEntity);
                        _context.SaveChangesAsync();
                        var result = new TokenInfoDto();
                        result.AccessToken = _tokenService.GenerateBearerToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());
                        result.RefreshToken = _tokenService.GenerateRefreshToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());
                        

                        return result;
                    }

                    else
                    {
                        var newAccountEntity = new AccountEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = payload.Result.GivenName,
                            Surname = payload.Result.FamilyName,
                            Email = payload.Result.Email,
                            DateOfBirth = new DateTime(2008, 3, 1, 7, 0, 0),
                            IsDeleted = false,
                            RoleID = 2,
                            GoogleSubject = payload.Result.Subject
                        };

                        _context.Accounts.Add(newAccountEntity);
                        _context.SaveChangesAsync();
                        var result = new TokenInfoDto();
                        result.AccessToken = _tokenService.GenerateBearerToken(newAccountEntity.Id.ToString(), newAccountEntity.RoleID.ToString());
                        result.RefreshToken = _tokenService.GenerateRefreshToken(newAccountEntity.Id.ToString(), newAccountEntity.RoleID.ToString());

                        return result;

                    }
                }
                return null;
            }
        }

        public UserInfoDto GetUserInfoDto(AccountEntity accountEntity)
        {
            return new UserInfoDto
            {
                Name = accountEntity.Name,
                Surname = accountEntity.Surname,
                Email = accountEntity.Email,
                DateOfBirth = accountEntity.DateOfBirth,
                Login = accountEntity.Login,
            };

        }

        public string HashPassword(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task<IActionResult> GetAllUsers(PaginationDto paginationDto)
        {
            try
            {
                var data = _context.Accounts.AsQueryable().Where(x => x.IsDeleted == false).OrderByDescending(x => x.DateCreated);


                return await GetPaginatedUsers(paginationDto, data);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetPaginatedUsers(PaginationDto paginationDto, IQueryable<AccountEntity> data)
        {

            var totalCount = await data.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

            data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

            var userList = new List<dynamic>();

            foreach (var temp in await data.ToListAsync())
            {
                userList.Add(new
                {
                    UserID = temp.Id,
                    User = GetUserInfoDto(temp),
                    DateCreated = temp.DateCreated,

                });
                    
            }

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageIndex = paginationDto.pageIndex,
                PageSize = paginationDto.pageSize,
                Users = userList
            };
            return new OkObjectResult(result);
        }


        public async Task<IActionResult> AddAdmin(RegisterInfoDto registerInfoDto)
        {
            if (_context.Accounts == null)
            {
                return new NotFoundResult();
            }
            AccountEntity accountEntity = new AccountEntity
            {
                Id = Guid.NewGuid(),
                Name = registerInfoDto.Name,
                Surname = registerInfoDto.Surname,
                Login = registerInfoDto.Login,
                Email = registerInfoDto.Email,
                Password = HashPassword(registerInfoDto.Password),
                DateOfBirth = registerInfoDto.DateOfBirth,
                RoleID = 1
            };

            if (LoginEmailExists(accountEntity.Email, accountEntity.Login))
                return new NotFoundResult();
            _context.Accounts.Add(accountEntity);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}