using System.Security.Cryptography;
using System.Text;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore.Update.Internal;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class AccountService
    {
        private readonly TokenService _tokenService;
        private readonly DataContext _context;
        public AccountService(TokenService tokenService, DataContext context)
        {
            _tokenService = tokenService;
            _context = context;
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

        private bool EmailAlreadyExist(string email)
        {
            return (_context.Accounts?.Any(e => e.Email == email)).GetValueOrDefault();
        }
        private bool SubjectAlreadyExist(string subject)
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


    }
}