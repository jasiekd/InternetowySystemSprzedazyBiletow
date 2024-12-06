//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using QuickTickets.Api.Controllers;
//using QuickTickets.Api.Dto;
//using QuickTickets.Api.Services;
//using QuickTickets.Api.Settings;
//using QuickTickets.Tests.FakeServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace QuickTickets.Tests.Tests
//{
//    public class TransactionControllerTests
//    {
//        private ITokenService _tokenService;
//        private ITransactionService _transactionService;
//        private TransactionController _transactionController;

//        [SetUp]
//        public async Task Setup()
//        {
//            var tokenOptions = Options.Create(new TokenOptions
//            {
//                SigningKey = "iuirZ5QtqqCZM8Z65ZjJOMgFfgjHGdRgiuONXzb7XsRMOdKkzSqmF84hx8zAtvQW6bwC02MjddnxpoY9sDaGhBVdUZOeBFFa19dnLBCJR3vnHZdI89jdqV7TssyMSc09azWuhajNVx201liS8xQGgdojIbnzajD0d83Hr0q8nmvKGsWqLAWhdfdeIMjP3BIwGyQIgnTHhkQgrfyrPYjKNpOaey2KIgffFOxgNeBk15cOIsg5MPHO0P1C2YPaq",
//                Issuer = "api.bearer.auth",
//                Audience = "api.bearer.auth"
//            });

//            _tokenService = new FakeTokenService(tokenOptions);
//            _transactionService = new FakeTransactionService();
//            _transactionController = new TransactionController(_transactionService);

//        }

//        [Test]
//        public async Task CreateTransaction_ValidData_ReturnsOkResult_CheckContainsDotPayLink()
//        {
//            string token = _tokenService.GenerateBearerToken("2EF422AE-0E8E-4F47-93BB-8B79F04123B6", "1");
//            var claims = new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, "2EF422AE-0E8E-4F47-93BB-8B79F04123B6"),
//                new Claim(ClaimTypes.Role, "1")
//            };
//            var identity = new ClaimsIdentity(claims);
//            var claimsPrincipal = new ClaimsPrincipal(identity);

//            _transactionController.ControllerContext = new ControllerContext
//            {
//                HttpContext = new DefaultHttpContext
//                {
//                    User = claimsPrincipal
//                }
//            };

//            var data = new TransactionRequestDto
//            {
//                Cost = 1,
//                Desc = "Zaplata za bilet",
//                EventID = 1,
//                NumberOfTickets = 1,
//            };

//            var result = await _transactionController.CreateTransaction(data);

//            Assert.IsInstanceOf<OkObjectResult>(result);
//            var okResult = (OkObjectResult)result;
//            var paymentLink = (string)okResult.Value;
//            Assert.IsNotNull(paymentLink);
//            Assert.IsTrue(paymentLink.Contains("dotpay"));
//        }
//    }
//}
