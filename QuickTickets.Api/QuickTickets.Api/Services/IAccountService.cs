using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using System.Security.Cryptography;
using System.Text;

namespace QuickTickets.Api.Services
{
    public interface IAccountService
    {
        public Task<IActionResult> Update(RegisterInfoDto registerInfoDto, Guid accountId);
        public Task<UserInfoDto> GetAccount(Guid accountId);
        public Task<IActionResult> Register(RegisterInfoDto registerInfoDto);
        public TokenInfoDto LoginUser(UserLoginRequestDto loginData);
        public Task<IActionResult> Delete(Guid accountID);
        public Task<IActionResult> GetListOfUsers(PaginationDto paginationDto);
        public TokenInfoDto LoginUserWithGoogle(GoogleLoginRequestDto loginData);
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
        //public Task<IActionResult> GetAllUsers(PaginationDto paginationDto);

        //public Task<IActionResult> GetPaginatedUsers(PaginationDto paginationDto, IQueryable<AccountEntity> data);

    }
}
