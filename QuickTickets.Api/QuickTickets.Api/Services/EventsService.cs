using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuickTickets.Api.Services
{
    public class EventsService : IEventsService
    {
        private readonly DataContext _context;
        private readonly ITrackUserMovesService _trackUserMovesService;
        public EventsService(DataContext context, ITrackUserMovesService trackUserMovesService)
        {
            _context = context;
            _trackUserMovesService = trackUserMovesService;
        }
        private bool EventsEntityExists(long id)
        {
            return (_context.Events?.Any(e => e.EventID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> PostEventEntity(CreateEventDto createEventDto, Guid userId)
        {
            if (_context.Events == null)
            {
                return new NotFoundResult();
            }

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

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<EventInfoDto>> GetEvent(long id, Guid userId)
        {
            if (_context.Events == null)
            {
                return new NotFoundResult();
            }
            EventInfoDto eventInfo = await GetEventInfo(id);

            if (eventInfo == null)
            {
                return new NotFoundResult();
            }
            if (userId != Guid.Empty)
            {
                await _trackUserMovesService.Add(new AddUserEventHistoryRequestDto
                {
                    EventID = id,
                    Label = (float)UserEventHistoryLabelEnum.Visited
                }, userId);
            }

            return new OkObjectResult(eventInfo);
        }

        public async Task<ActionResult<IEnumerable<EventInfoDto>>> getHotEvents()
        {
            if (_context.Events == null)
            {
                return new NotFoundResult();
            }
            var eventsInfo = await getHotEventsInfo();

            if (eventsInfo == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(eventsInfo);
        }

        public async Task<ActionResult<IEnumerable<LocationsEntity>>> getHotLocations()
        {
            if (_context.Events == null)
            {
                return new NotFoundResult();
            }
            var eventsInfo = await getHotLocationsInfo();

            if (eventsInfo == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(eventsInfo);
        }
        public async Task<IActionResult> SearchEvents([FromBody] SearchEventDto searchEventDto)
        {
            var result = await GetForSearch(searchEventDto);
            return new OkObjectResult(result);
        }
        public async Task<IActionResult> AcceptEvent([FromBody] long id)
        {
            EventsEntity events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return new NotFoundResult();
            }
            events.Status = StatusEnum.Confirmed.ToString();
            events.DateModified = DateTime.Now;

            _context.Events.Update(events);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public async Task<IActionResult> CancelEvent([FromBody] long id)
        {
            EventsEntity events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return new NotFoundResult();
            }
            events.Status = StatusEnum.Cancelled.ToString();
            events.DateModified = DateTime.Now;

            _context.Events.Update(events);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public async Task<IActionResult> GetPendingEventsAction(PaginationDto paginationDto)
        {
            var result = await GetPendingEvents(paginationDto);
            return new OkObjectResult(result);
        }
        public async Task<IActionResult> GetOrganisatorEventsAction(PaginationDto paginationDto, string statusChoice, Guid userId)
        {
            var result = await GetOrganisatorEvents(paginationDto, userId, statusChoice);
            return new OkObjectResult(result);
        }
        public async Task<IActionResult> UpdateEvent(CreateEventDto createEventDto, long eventID)
        {
            if (!EventsEntityExists(eventID))
            {
                return new NotFoundResult();
            }
            var data = await _context.Events.FindAsync(eventID);
            if (data.Status != StatusEnum.Pending.ToString())
            {
                return new BadRequestObjectResult("Status nie jest pending");
            }
            data.AdultsOnly = createEventDto.AdultsOnly;
            data.TicketPrice = createEventDto.TicketPrice;
            data.LocationID = createEventDto.LocationID;
            data.Date = createEventDto.Date;
            data.DateModified = DateTime.Now;
            data.Description = createEventDto.Description;
            data.TypeID = createEventDto.TypeID;
            data.ImgURL = createEventDto.ImgURL;
            data.Title = createEventDto.Title;
            data.Seats = createEventDto.Seats;


            try
            {
                _context.Events.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsEntityExists(eventID))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return new OkResult();
        }
        public async Task<EventInfoDto> GetEventInfo(long id)
        {
            var eventsEntity = await _context.Events.Include(x => x.Type).Include(x => x.Location).Include(x => x.Owner).Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString()).FirstOrDefaultAsync(x => x.EventID == id);

            if(eventsEntity == null)
            {
                return null;
            }
            
            return GetEventInfoDto(eventsEntity);
        }
        public async Task<IEnumerable<EventInfoDto>> getHotEventsInfo()
        {
            var eventsEntity = await _context.Events.Include(x => x.Owner).Include(x => x.Type).Include(x => x.Location).Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString()).OrderByDescending(x => x.Date).ToListAsync();

            if (eventsEntity == null || !eventsEntity.Any())
            {
                return null;
            }

            List<Tuple<EventsEntity, float>> tmpList = new();

            foreach (var eventEntity in eventsEntity)
            {
                int ticketsCount = await GetCountTicketForTransaction(eventEntity.EventID);
                var occupiedPercentage = CalculateOccupiedPercentage(ticketsCount, eventEntity.Seats);
                var dateDifference = CalculateDateDifference(eventEntity.Date);

                var attractivenessScore = occupiedPercentage + dateDifference;

                tmpList.Add(new Tuple<EventsEntity, float>(eventEntity, attractivenessScore));
            }

            var hotEvents = tmpList.OrderByDescending(x => x.Item2).Take(4).ToList();

            var ListOfEventsInfo = new List<EventInfoDto>();

            foreach (var temp in hotEvents)
            {
                ListOfEventsInfo.Add(GetEventInfoDto(temp.Item1));
            }
            return ListOfEventsInfo;
        }
        private float CalculateOccupiedPercentage(int ticketsCount, int totalSeats)
        {
            if (totalSeats == 0) return 0;
            return (float)ticketsCount / totalSeats * 100;
        }
        private async Task<int> GetCountTicketForTransaction(long eventID)
        {
            var countTickets = await _context.Tickets.Where(x => x.EventID == eventID).CountAsync();
            return countTickets;
        }
        private float CalculateDateDifference(DateTime eventDate)
        {
            var today = DateTime.Now;
            var dateDifference = (eventDate - today).Days;

            if (dateDifference < 0)
            {
                return 0;
            }

            return Math.Max(0, 30 - dateDifference); 
        }
        public async Task<IEnumerable<LocationsEntity>> getHotLocationsInfo()
        {
            var topLocationsEvent = await _context.Events
                .Include(x => x.Location)
                .Where(x => x.IsActive == true)
                .GroupBy(x => x.LocationID)
                .Select(g => new
                {
                    LocationID = g.Key,
                    LocationName = g.First().Location.Name,
                    LocationDescription = g.First().Location.Description,
                    LocationImgURL = g.First().Location.ImgURL,
                    EventCount = g.Count()
                })
                .OrderByDescending(x => x.EventCount)
                .Take(4)
                .ToListAsync();
            if (topLocationsEvent == null)
            {
                return null;
            }

            var ListOfLocationInfo = new List<LocationsEntity>();

            foreach (var temp in topLocationsEvent)
            {
                ListOfLocationInfo.Add(new LocationsEntity
                {
                    LocationID= temp.LocationID,
                    Name = temp.LocationName,
                    Description= temp.LocationDescription,
                    ImgURL = temp.LocationImgURL,
                });
            }
            return ListOfLocationInfo;

        }

        private int CountAvailableSeats(int seats, long id)
        {
            int data = _context.Tickets.AsQueryable().Include(x => x.Transaction).Where(t => (t.EventID == id && t.Transaction.Status == StatusEnum.Paid.ToString()) || (t.EventID == id && t.Transaction.Status == StatusEnum.AdminOffPaid.ToString())).Sum(t => t.Amount);
            int availableSeats = seats - data;
            return availableSeats;
        }

        public async Task<IActionResult> GetForSearch(SearchEventDto searchEventDto)
        {
            try
            {
                var data = _context.Events.AsQueryable().Include(e => e.Type).Include(x => x.Location).Include(x => x.Owner).Where(e => e.IsActive == true && e.Status == StatusEnum.Confirmed.ToString());

                if(!string.IsNullOrEmpty(searchEventDto.searchPhrase))
                {
                    data = data.Where(e =>
                    e.Title.Contains(searchEventDto.searchPhrase) ||
                    e.Description.Contains(searchEventDto.searchPhrase));
                }

                if (searchEventDto.minPrice.HasValue)
                {
                    data = data.Where(e => e.TicketPrice >= searchEventDto.minPrice.Value);
                }

                if (searchEventDto.maxPrice.HasValue)
                {
                    data = data.Where(e => e.TicketPrice <= searchEventDto.maxPrice.Value);
                }

                if (searchEventDto.startDate.HasValue)
                {
                    data = data.Where(e => e.Date >= searchEventDto. startDate.Value);
                }

                if (searchEventDto.endDate.HasValue)
                {
                    data = data.Where(e => e.Date <= searchEventDto.endDate.Value);
                }

                if (searchEventDto.locationId.HasValue)
                {
                    data = data.Where(e => e.LocationID == searchEventDto.locationId.Value);
                }

                if (searchEventDto.typeId.HasValue)
                {
                    data = data.Where(e => e.TypeID == searchEventDto.typeId.Value);
                }
                data = data.OrderByDescending(e => e.Date);
                return await GetPaginatedEvents(searchEventDto, data);

            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetPendingEvents(PaginationDto paginationDto)
        {
            try
            {
                var data = _context.Events.AsQueryable().Include(e => e.Type).Include(x => x.Location).Include(x => x.Owner).Where(e => e.Status == StatusEnum.Pending.ToString());


                return await GetPaginatedEvents(paginationDto, data);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetOrganisatorEvents(PaginationDto paginationDto, Guid guid, string statusChoice)
        {
            try
            {
                var data = _context.Events.AsQueryable().Include(e => e.Type).Include(x => x.Location).Include(x => x.Owner).Where(e => e.Status == statusChoice && e.OwnerID == guid);


                return await GetPaginatedEvents(paginationDto, data);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetPaginatedEvents(PaginationDto paginationDto, IQueryable<EventsEntity> data)
        {

            var totalCount = await data.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

            data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

            List<EventInfoDto> eventList = new List<EventInfoDto>();

            foreach(var temp in await data.ToListAsync())
            {
                eventList.Add(GetEventInfoDto(temp));
            }

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageIndex = paginationDto.pageIndex,
                PageSize = paginationDto.pageSize,
                Events = eventList
            };
            return new OkObjectResult(result);
        }
        public EventInfoDto GetEventInfoDto(EventsEntity eventsEntity)
        {
            return new EventInfoDto
            {
                EventID = eventsEntity.EventID,
                Title = eventsEntity.Title,
                Seats = eventsEntity.Seats,
                AvailableSeats = CountAvailableSeats(eventsEntity.Seats, eventsEntity.EventID),
                TicketPrice = eventsEntity.TicketPrice,
                Description = eventsEntity.Description,
                Date = eventsEntity.Date,
                IsActive = eventsEntity.IsActive,
                AdultsOnly = eventsEntity.AdultsOnly,
                Type = eventsEntity.Type,
                Location = eventsEntity.Location,
                ImgURL = eventsEntity.ImgURL,
                Name = eventsEntity.Owner.Name,
                Surname = eventsEntity.Owner.Surname,
                Email = eventsEntity.Owner.Email,
             };
            
        }
        public async Task<IActionResult> CanBuyTicket(long eventID)
        {
            var data = await _context.Events.FindAsync(eventID);
            if (data.Date < DateTime.Now)
            {
                data.Status = StatusEnum.Cancelled.ToString();
                data.DateModified = DateTime.Now;

                _context.Events.Update(data);
                await _context.SaveChangesAsync();
                return new NotFoundResult();
            }
            return new OkResult();
        }
    }
}
