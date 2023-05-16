using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly EventsService _eventsService;

        public EventsController(DataContext context, EventsService eventsService)
        {
            _context = context;
            _eventsService = eventsService;
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> PostEventEntity(CreateEventDto createEventDto)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            //Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Guid userId = Guid.Parse("7A8D0B4E-5D46-4E9D-AB7B-6BC665B11953");
            EventsEntity eventEntity = new EventsEntity { 
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

            return Ok();
        }

        [HttpGet("getEvent/{id}")]
        public async Task<ActionResult<EventInfoDto>> GetEvent(long id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            EventInfoDto eventInfo = await _eventsService.GetEventInfo(id);

            if(eventInfo == null)
            {
                return NotFound();
            }

            return Ok(eventInfo);
        }

        [HttpGet("getHotEvents/")]
        public async Task<ActionResult<IEnumerable<EventInfoDto>>> getHotEvents()
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var eventsInfo = await _eventsService.getHotEventsInfo();

            if (eventsInfo == null)
            {
                return NotFound();
            }

            return Ok(eventsInfo);
        }

        [HttpGet("getHotLocations/")]
        public async Task<ActionResult<IEnumerable<LocationsEntity>>> getHotLocations()
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var eventsInfo = await _eventsService.getHotLocationsInfo();

            if (eventsInfo == null)
            {
                return NotFound();
            }

            return Ok(eventsInfo);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventsEntity>> GetEventsEntity(long id)
        {
          if (_context.Events == null)
          {
              return NotFound();
          }
            var eventsEntity = await _context.Events.Include(x => x.Type).Include(x => x.Location).FirstOrDefaultAsync(x => x.EventID == id);

            if (eventsEntity == null)
            {
                return NotFound();
            }

            return eventsEntity;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventsEntity(long id, EventsEntity eventsEntity)
        {
            if (id != eventsEntity.EventID)
            {
                return BadRequest();
            }

            _context.Entry(eventsEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventsEntity>> PostEventsEntity(EventsEntity eventsEntity)
        {
          if (_context.Events == null)
          {
              return Problem("Entity set 'DataContext.Events'  is null.");
          }
            _context.Events.Add(eventsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventsEntity", new { id = eventsEntity.EventID }, eventsEntity);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventsEntity(long id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var eventsEntity = await _context.Events.FindAsync(id);
            if (eventsEntity == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventsEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventsEntityExists(long id)
        {
            return (_context.Events?.Any(e => e.EventID == id)).GetValueOrDefault();
        }
    }
}
