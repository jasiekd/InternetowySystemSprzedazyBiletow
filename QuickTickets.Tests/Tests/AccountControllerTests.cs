using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuickTickets.Api.Controllers;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;
using QuickTickets.Api.Settings;
using QuickTickets.Tests.FakeServices;
using System.Globalization;

namespace QuickTickets.Tests.Tests
{
    public class AccountControllerTests
    {
        private ITokenService _tokenService;
        private IAccountService _accountService;
        private AccountController _accountController;

        [SetUp]
        public async Task Setup()
        {
            var tokenOptions = Options.Create(new TokenOptions
            {
                SigningKey ="iuirZ5QtqqCZM8Z65ZjJOMgFfgjHGdRgiuONXzb7XsRMOdKkzSqmF84hx8zAtvQW6bwC02MjddnxpoY9sDaGhBVdUZOeBFFaX19dnLBCJR3vnHZdI89jdqV7TssyMSc09azWuhajNVx201liS8xQGgdojIbnzajD0d83H0q8nmvKGsWqLAWhdfdeIMjP3BIwG2yQIgnTHhkQgrfyrPYjKNpOaey2KIgffFOxgNeBk15cOIsg5MPHO0P1C2YPaq",
                Issuer = "api.bearer.auth",
                Audience = "api.bearer.auth"
            });

            _tokenService = new FakeTokenService(tokenOptions);
            _accountService = new FakeAccountService(_tokenService);
            _accountController = new AccountController(_accountService);
        }

        [Test]
        public async Task RegisterUser_ValidData_ReturnsOkResult()
        {
            var registerData = new RegisterInfoDto
            {
                Name = "Artur",
                Surname = "Graba",
                Email = "agraba@cos.nie",
                Login = "agrabson",
                Password = "admin",
                DateOfBirth = DateTime.ParseExact("04-11-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            };

            var result = await _accountController.PostAccountEntity(registerData);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task LoginUser_NoSuchUser_ReturnsNotFound()
        {
            UserLoginRequestDto loginData = new UserLoginRequestDto
            {
                UserName = "johanson",
                Password = "zaq1@WSX",
            };
            var result = await _accountController.LoginUser(loginData);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public async Task LoginUser_ValidData_ReturnsOkObjectResult()
        {
            UserLoginRequestDto loginData = new UserLoginRequestDto
            {
                UserName = "agardian",
                Password = "zaq1@WSX",
            };
            var result = await _accountController.LoginUser(loginData);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var tokenInfo = (TokenInfoDto)okResult.Value;

            Assert.IsNotNull(tokenInfo);
        }
    }
}