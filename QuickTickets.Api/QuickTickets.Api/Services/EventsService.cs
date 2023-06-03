using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuickTickets.Api.Services
{
    public class EventsService
    {
        private readonly DataContext _context;
        public EventsService(DataContext context)
        {
            _context = context;
        }

        public async Task<EventInfoDto> GetEventInfo(long id)
        {
            var eventsEntity = await _context.Events.Include(x => x.Type).Include(x => x.Location).Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString()).FirstOrDefaultAsync(x => x.EventID == id);

            if(eventsEntity == null)
            {
                return null;
            }

            EventInfoDto eventInfoDto = new EventInfoDto { 
                EventID = id,
                Title = eventsEntity.Title,
                Seats = eventsEntity.Seats,
                OccupiedSeats = CountOccupiedSeats(eventsEntity.Seats, id),
                TicketPrice= eventsEntity.TicketPrice,
                Description= eventsEntity.Description,
                Date= eventsEntity.Date,
                IsActive= eventsEntity.IsActive,
                AdultsOnly= eventsEntity.AdultsOnly,
                Type = eventsEntity.Type,
                Location = eventsEntity.Location,
                ImgURL= eventsEntity.ImgURL,
                OwnerID= eventsEntity.OwnerID
            };

            return eventInfoDto;
        }

        public async Task<IEnumerable<EventInfoDto>> getHotEventsInfo()
        {
            var eventsEntity = await _context.Events.Include(x => x.Type).Include(x => x.Location).Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString()).OrderByDescending(x => x.Date).Take(4).ToListAsync();
            if (eventsEntity == null)
            {
                return null;
            }

            var ListOfEventsInfo = new List<EventInfoDto>();

            foreach(var temp in eventsEntity)
            {
                ListOfEventsInfo.Add(new EventInfoDto
                {
                    EventID = temp.EventID,
                    Title = temp.Title,
                    Seats = temp.Seats,
                    OccupiedSeats = CountOccupiedSeats(temp.Seats, temp.EventID),
                    TicketPrice = temp.TicketPrice,
                    Description = temp.Description,
                    Date = temp.Date,
                    IsActive = temp.IsActive,
                    AdultsOnly = temp.AdultsOnly,
                    Type = temp.Type,
                    Location = temp.Location,
                    ImgURL = temp.ImgURL,
                    OwnerID = temp.OwnerID
                });
            }
            return ListOfEventsInfo;

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

        private int CountOccupiedSeats(int seats, long id)
        {
            //zliczenie ticketow dla danego id wydarzenia
            int occupiedSeats = 10;
            return occupiedSeats;
        }

        public async Task<IActionResult> GetForSearch(SearchEventDto searchEventDto)
        {
            try
            {
                var data = _context.Events.AsQueryable().Where(e => e.IsActive == true && e.Status == StatusEnum.Confirmed.ToString());

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

                var totalCount = await data.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)searchEventDto.pageSize);

                data = data.Skip((searchEventDto.pageIndex - 1) * searchEventDto.pageSize).Take(searchEventDto.pageSize);

                var events = await data.ToListAsync();

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageIndex = searchEventDto.pageIndex,
                    PageSize = searchEventDto.pageSize,
                    Events = events
                };

                return new OkObjectResult(result);

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
                var data = _context.Events.AsQueryable().Where(e => e.Status == StatusEnum.Pending.ToString());


                var totalCount = await data.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

                data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

                var events = await data.ToListAsync();

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageIndex = paginationDto.pageIndex,
                    PageSize = paginationDto.pageSize,
                    Events = events
                };

                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
