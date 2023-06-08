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
            var eventsEntity = await _context.Events.Include(x => x.Type).Include(x => x.Location).Include(x => x.Owner).Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString()).FirstOrDefaultAsync(x => x.EventID == id);

            if(eventsEntity == null)
            {
                return null;
            }
            
            return GetEventInfoDto(eventsEntity);
        }

        public async Task<IEnumerable<EventInfoDto>> getHotEventsInfo()
        {
            var eventsEntity = await _context.Events.Include(x => x.Owner).Include(x => x.Type).Include(x => x.Location).Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString()).OrderByDescending(x => x.Date).Take(4).ToListAsync();
            if (eventsEntity == null)
            {
                return null;
            }

            var ListOfEventsInfo = new List<EventInfoDto>();

            foreach(var temp in eventsEntity)
            {
                ListOfEventsInfo.Add(GetEventInfoDto(temp));
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
                OccupiedSeats = CountOccupiedSeats(eventsEntity.Seats, eventsEntity.EventID),
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



    }
}
