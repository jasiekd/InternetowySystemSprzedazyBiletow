using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace QuickTickets.Tests.FakeServices
{
    public class FakeAccountService : IAccountService
    {
        List<AccountEntity> _accounts;
        ITokenService _tokenService;
        public FakeAccountService(ITokenService tokenService) {
            _tokenService = tokenService;
            string password = HashPassword("zaq1@WSX");
            _accounts = new List<AccountEntity>()
            {
                new AccountEntity
                {
                    Id = Guid.Parse("2EF422AE-0E8E-4F47-93BB-8B79F04123B6"),
                    Name = "Artur",
                    Surname = "Gardian",
                    Email = "agardian00@cos.nie",
                    Login = "agardian",
                    Password = password,
                    DateCreated= DateTime.Now,
                    DateOfBirth= DateTime.ParseExact("04-11-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted= false,
                    RoleID = 1,
                },
                new AccountEntity
                {
                    Id = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87"),
                    Name = "Jan",
                    Surname = "Kowalski",
                    Email = "jkowalski01@cos.nie",
                    Login = "jkowalski",
                    Password = password,
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.ParseExact("11-06-2002", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    RoleID = 3,
                }
            };
        }

        public string HashPassword(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public async Task<IActionResult> Update(RegisterInfoDto registerInfoDto, Guid accountId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserInfoDto> GetAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> Register(RegisterInfoDto registerInfoDto)
        {
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
            _accounts.Add(accountEntity);

            return new OkResult();
        }
        private bool LoginEmailExists(string email, string login)
        {
            return _accounts.Any(e => e.Email == email || e.Login == login);
        }
        public TokenInfoDto LoginUser(UserLoginRequestDto loginData)
        {
            var accountEntity = _accounts.Where(x => x.Login == loginData.UserName && x.IsDeleted == false).FirstOrDefault();
            if (accountEntity != null)
            {
                string password = HashPassword(loginData.Password);
                if (accountEntity.Password == password)
                {
                    var result = new TokenInfoDto();
                    result.AccessToken = _tokenService.GenerateBearerToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());
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
        public async Task<IActionResult> Delete(Guid accountID)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> GetListOfUsers(PaginationDto paginationDto)
        {
            return new OkObjectResult(_accounts);
        }
        public TokenInfoDto LoginUserWithGoogle(GoogleLoginRequestDto loginData)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> AddAdmin(RegisterInfoDto registerInfoDto)
        {
            throw new NotImplementedException();
        }
    }
}
