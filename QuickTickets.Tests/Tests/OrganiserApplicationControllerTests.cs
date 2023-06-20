using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuickTickets.Api.Controllers;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;
using QuickTickets.Api.Settings;
using QuickTickets.Tests.FakeServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuickTickets.Tests.Tests
{
    public class OrganiserApplicationControllerTests
    {
        private ITokenService _tokenService;
        private IOrganiserApplicationService _organiserApplicationService;
        private OrganiserApplicationController _organiserApplicationController;

        [SetUp]
        public async Task Setup()
        {
            var tokenOptions = Options.Create(new TokenOptions
            {
                SigningKey = "iuirZ5QtqqCZM8Z65ZjJOMgFfgjHGdRgiuONXzb7XsRMOdKkzSqmF84hx8zAtvQW6bwC02MjddnxpoY9sDaGhBVdUZOeBFFa19dnLBCJR3vnHZdI89jdqV7TssyMSc09azWuhajNVx201liS8xQGgdojIbnzajD0d83Hr0q8nmvKGsWqLAWhdfdeIMjP3BIwGyQIgnTHhkQgrfyrPYjKNpOaey2KIgffFOxgNeBk15cOIsg5MPHO0P1C2YPaq",
                Issuer = "api.bearer.auth",
                Audience = "api.bearer.auth"
            });

            _tokenService = new FakeTokenService(tokenOptions);
            _organiserApplicationService = new FakeOrganiserApplicationService();
            _organiserApplicationController = new OrganiserApplicationController(_organiserApplicationService);
            
        }

        [Test]
        public async Task SendOrganiserApplication_ValidData_ReturnsOkResult()
        {
            string token = _tokenService.GenerateBearerToken("2EF422AE-0E8E-4F47-93BB-8B79F04123B6", "1");
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "2EF422AE-0E8E-4F47-93BB-8B79F04123B6"),
                new Claim(ClaimTypes.Role, "1")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _organiserApplicationController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            var data = new OrganiserApplicationDto
            {
                Description = "Test",
                UserId = Guid.Parse("2EF422AE-0E8E-4F47-93BB-8B79F04123B6")
            };

            var result = await _organiserApplicationController.SendOrganiserApplication(data);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
