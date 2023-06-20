using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTickets.Tests.FakeServices
{
    public class FakeEventsService : IEventsService
    {
        public List<EventsEntity> _events;
        public FakeEventsService() 
        { 
            _events = new List<EventsEntity>() {
                new EventsEntity
                {
                    EventID = 1,
                    Title = "Ludzie trzymajcie spodnie",
                    Seats = 123,
                    TicketPrice = 10,
                    Description = "\"Ludzie trzymajcie kapelusze\" to mój drugi solowy program, grany od grudnia 2016 do sierpnia 2017 roku.  Udostępniony materiał został zarejestrowany 10 lipca 2017 roku w gdańskim klubie \"Parlament\". Obok mnie na scenie pojawił się również Adam Van Bendler.",
                    Date = DateTime.ParseExact("30-06-2023 18:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = true,
                    TypeID = 7,
                    LocationID = 2,
                    ImgURL = "https://images.unsplash.com/photo-1610964199131-5e29387e6267?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1932&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                },
                new EventsEntity
                {
                    EventID = 2,
                    Title = "Spływ kajakowy",
                    Seats = 60,
                    TicketPrice = 25,
                    Description = "Spływ kajakiem po rzece Morawka",
                    Date = DateTime.ParseExact("01-07-2023 12:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = false,
                    TypeID = 3,
                    LocationID = 6,
                    ImgURL = "https://images.unsplash.com/photo-1472745942893-4b9f730c7668?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1169&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                },
                new EventsEntity
                {
                    EventID = 3,
                    Title = "Recital Pani Żak",
                    Seats = 100,
                    TicketPrice = 15,
                    Description = "W swoim wykonaniu Pani Żak zaprezentuje swoje umiejętności artystyczne.",
                    Date = DateTime.ParseExact("01-07-2023 18:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = false,
                    TypeID = 1,
                    LocationID = 5,
                    ImgURL = "https://images.unsplash.com/photo-1521116103845-2170f3377fec?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                },
                new EventsEntity
                {
                    EventID = 4,
                    Title = "Targi pracy",
                    Seats = 600,
                    TicketPrice = 50,
                    Description = "W naszej ofercie po prostu tak jakby przedstawimy oferty grona firm mówiących o swoich zapotrzebowaniach i planach dla widza.",
                    Date = DateTime.ParseExact("02-07-2023 12:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = true,
                    TypeID = 6,
                    LocationID = 1,
                    ImgURL = "https://images.unsplash.com/photo-1618092388874-e262a562887f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1025&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                }
                ,
                new EventsEntity
                {
                    EventID = 5,
                    Title = "Targi pracy",
                    Seats = 600,
                    TicketPrice = 50,
                    Description = "W naszej ofercie po prostu tak jakby przedstawimy oferty grona firm mówiących o swoich zapotrzebowaniach i planach dla widza.",
                    Date = DateTime.ParseExact("02-07-2023 12:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = true,
                    TypeID = 6,
                    LocationID = 1,
                    ImgURL = "https://images.unsplash.com/photo-1618092388874-e262a562887f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1025&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                }
            };
        }

        public async Task<IActionResult> PostEventEntity(CreateEventDto createEventDto, Guid userId)
        {

            EventsEntity eventEntity = new EventsEntity
            {
                EventID = 0,
                Title = createEventDto.Title,
                Seats = createEventDto.Seats,
                TicketPrice = createEventDto.TicketPrice,
                Description = createEventDto.Description,
                Date = createEventDto.Date,
                IsActive = createEventDto.IsActive,
                AdultsOnly = createEventDto.AdultsOnly,
                TypeID = createEventDto.TypeID,
                LocationID = createEventDto.LocationID,
                ImgURL = createEventDto.ImgURL,
                OwnerID = userId
            };

            _events.Add(eventEntity);
            return new OkResult();
        }
        public async Task<ActionResult<EventInfoDto>> GetEvent(long id)
        {
            if(_events.Any(e => e.EventID != id))
            {
                return new NotFoundResult();
            }

            EventInfoDto eventInfo = GetEventInfoDto(_events.Find(x => x.EventID == id));

            return new OkObjectResult(eventInfo);
        }
        public async Task<ActionResult<IEnumerable<EventInfoDto>>> getHotEvents()
        {
            var eventsEntity = _events.Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString()).OrderByDescending(x => x.Date).Take(4);
            if (eventsEntity == null)
            {
                return new NoContentResult();
            }

            var ListOfEventsInfo = new List<EventInfoDto>();

            foreach (var temp in eventsEntity)
            {
                ListOfEventsInfo.Add(GetEventInfoDto(temp));
            }
            return new OkObjectResult(ListOfEventsInfo);
        }
        public Task<ActionResult<IEnumerable<LocationsEntity>>> getHotLocations()
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> SearchEvents(SearchEventDto searchEventDto)
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> AcceptEvent(long id)
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> CancelEvent(long id)
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> GetPendingEventsAction(PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> GetOrganisatorEventsAction(PaginationDto paginationDto, string statusChoice, Guid userId)
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> UpdateEvent(CreateEventDto createEventDto, long eventID)
        {
            throw new NotImplementedException();
        }
        public EventInfoDto GetEventInfoDto(EventsEntity eventsEntity)
        {
            return new EventInfoDto
            {
                EventID = eventsEntity.EventID,
                Title = eventsEntity.Title,
                Seats = eventsEntity.Seats,
                AvailableSeats = 0,
                TicketPrice = eventsEntity.TicketPrice,
                Description = eventsEntity.Description,
                Date = eventsEntity.Date,
                IsActive = eventsEntity.IsActive,
                AdultsOnly = eventsEntity.AdultsOnly,
                Type = null,
                Location = null,
                ImgURL = eventsEntity.ImgURL,
                Name = null,
                Surname = null,
                Email = null,
            };

        }
        public Task<IActionResult> CanBuyTicket(long eventID)
        {
            throw new NotImplementedException();
        }
    }
}
