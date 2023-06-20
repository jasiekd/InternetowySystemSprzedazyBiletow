using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Common;
using QuickTickets.Api.Controllers;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using QuickTickets.Api.Settings;
using QuickTickets.Tests.FakeServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Data;

namespace QuickTickets.Tests.Tests
{
    public class EventsControllerTests
    {
        private ITokenService _tokenService;
        private IEventsService _eventsService;
        private EventsController _eventsController;

        [SetUp]
        public async Task Setup()
        {
            var tokenOptions = Options.Create(new TokenOptions
            {
                SigningKey ="iuirZ5QtqqCZM8Z65ZjJOMgFfgjHGdRgiuONXzb7XsRMOdKkzSqmF84hx8zAtvQW6bwC02MjddnxpoY9sDaGhBVdUZOeBFFa19dnLBCJR3vnHZdI89jdqV7TssyMSc09azWuhajNVx201liS8xQGgdojIbnzajD0d83Hr0q8nmvKGsWqLAWhdfdeIMjP3BIwGyQIgnTHhkQgrfyrPYjKNpOaey2KIgffFOxgNeBk15cOIsg5MPHO0P1C2YPaq",
                Issuer = "api.bearer.auth",
                Audience = "api.bearer.auth"
            });

            _tokenService = new FakeTokenService(tokenOptions);
            _eventsService = new FakeEventsService();
            _eventsController = new EventsController(_eventsService);
            string token = _tokenService.GenerateBearerToken("2EF422AE-0E8E-4F47-93BB-8B79F04123B6", "1");
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "2EF422AE-0E8E-4F47-93BB-8B79F04123B6"),
                new Claim(ClaimTypes.Role, "1")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _eventsController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
        }

        [Test]
        public async Task AddEvent_ValidData_ReturnsOkResult()
        {
            
            var eventData = new CreateEventDto
            {
                Title = "Ludzie trzymajcie kapelusze",
                Seats = 15,
                TicketPrice = 5,
                Description = "Super stand-up",
                Date = DateTime.ParseExact("04-11-2024", "dd-MM-yyyy",CultureInfo.InvariantCulture),
                IsActive = true,
                AdultsOnly = false,
                TypeID = 7,
                LocationID = 2,
                ImgURL = "https://iili.io/Hr6wwKP.jpg",
            };
            var result = await _eventsController.PostEventEntity(eventData);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task GetHotEvents_ReturnsListOfEventInfoDto_CountsOK()
        {
            var result = await _eventsController.getHotEvents();
            Assert.IsInstanceOf<ActionResult<IEnumerable<EventInfoDto>>>(result);
            var actionResult = (ActionResult<IEnumerable<EventInfoDto>>)result;
            var okObjectResult = (OkObjectResult)actionResult.Result;
            var eventsInfo = (IEnumerable<EventInfoDto>)okObjectResult.Value;

            Assert.AreEqual(4, eventsInfo.Count());
        }

    }
}
