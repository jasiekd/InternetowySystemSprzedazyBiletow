using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuickTickets.Api.Controllers;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;
using QuickTickets.Api.Settings;
using System.Globalization;

namespace QuickTickets.Tests.Tests
{
    public class AccountControllerTests
    {
        private DataContext _dataContext;
        private TokenService _tokenService;
        private AccountService _accountService;
        private AccountController _accountController;

        [SetUp]
        public async Task Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dataContext = new DataContext(dbContextOptions);

            var tokenOptions = Options.Create(new TokenOptions
            {
                SigningKey = "iuirZ5QtqqCZM8Z65ZjJOMgFfgjHGdRgiuONXzb7XsRMOdKkzSqmF84hx8zAtvQW6bwC02MjddnxpoY9sDaGhBVdUZOeBFFaX19dnLBCJR3vnHZdI89jdqV7TssyMSc09azWuhajNVx201liS8xQGgdojIbnzajD0d83Hr0q8nmvKGsWqLAWhdfdeIMjP3BIwG2yQIgnTHhkQgrfyrPYjKNpOaey2KIgffFOxgNeBk15cOIsg5MPHO0P1C2YPaq",
                Issuer = "api.bearer.auth",
                Audience = "api.bearer.auth"
            });

            _tokenService = new TokenService(tokenOptions);
            _accountService = new AccountService(_tokenService, _dataContext);
            _accountController = new AccountController(_accountService, _dataContext);
        }

        [Test]
        public async Task RegisterUser_ValidData_ReturnsCreatedAtActionResult()
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
            Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            Assert.AreEqual(201, (result.Result as CreatedAtActionResult)?.StatusCode);
        }

        [Test]
        public async Task LoginUser_NoSuchUser_ReturnsNotFound()
        {
            UserLoginRequestDto loginData = new UserLoginRequestDto
            {
                UserName = "johanson",
                Password = "zaq1@WSX",
            };
            var result2 = await _accountController.LoginUser(loginData);

            Assert.IsInstanceOf<NotFoundResult>(result2);
            var okResult = (NotFoundResult)result2;
            //Assert.AreEqual(result2, okResult.Value);

        }
    }
}