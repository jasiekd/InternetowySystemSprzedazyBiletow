using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuickTickets.Api.Controllers;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using QuickTickets.Api.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace QuickTickets.Tests.Tests
{
    public class EventsControllerTests
    {
        private DataContext _dataContext;
        private TokenService _tokenService;
        private EventsService _eventsService;
        private EventsController _eventsController;

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
            _eventsService = new EventsService(_dataContext);
            _eventsController = new EventsController(_dataContext, _eventsService);
        }

        [Test]
        public async Task gb()
        {
            var eventData = new CreateEventDto
            {
                Title = "Ludzie trzymajcie kapelusze",
                Seats = 15,
                TicketPrice = 5,
                Description = "Super stand-up",
                Date = DateTime.ParseExact("04-11-2024", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                IsActive = true,
                AdultsOnly = false,
                TypeID = 7,
                LocationID = 2,
                ImgURL = "https://iili.io/Hr6wwKP.jpg",

            };

            var result = await _eventsController.PostEventEntity(eventData);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

    }
}
