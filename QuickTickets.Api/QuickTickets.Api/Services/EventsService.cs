using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

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
            var eventsEntity = await _context.Events.Include(x => x.Type).Include(x => x.Location).FirstOrDefaultAsync(x => x.EventID == id);

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
            var eventsEntity = await _context.Events.Include(x => x.Type).Include(x => x.Location).Where(x => x.IsActive == true).OrderByDescending(x => x.Date).Take(4).ToListAsync();
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
                    Name = temp.LocationName
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
    }
}
