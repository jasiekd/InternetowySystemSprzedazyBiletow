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
        [Authorize]
        public async Task<IActionResult> PostEventEntity([FromBody] CreateEventDto createEventDto)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
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
        [AllowAnonymous]
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

        [HttpGet("getHotEvents")]
        [AllowAnonymous]
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

        [HttpGet("getHotLocations")]
        [AllowAnonymous]
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

        [HttpPost("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchEvents([FromBody]SearchEventDto searchEventDto)
        {
            var result = await _eventsService.GetForSearch(searchEventDto);
            return Ok(result);
        }

        [HttpPost("AcceptEvent")]
        [AdminAuthorize]
        public async Task<IActionResult> AcceptEvent([FromBody] long id)
        {
            EventsEntity events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }
            events.Status = StatusEnum.Confirmed.ToString();
            events.DateModified = DateTime.Now;

            _context.Events.Update(events);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("CancelEvent")]
        [AdminAuthorize]
        public async Task<IActionResult> CancelEvent([FromBody] long id)
        {
            EventsEntity events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }
            events.Status = StatusEnum.Cancelled.ToString();
            events.DateModified = DateTime.Now;

            _context.Events.Update(events);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("GetPendingEvents")]
        [AdminAuthorize]
        public async Task<IActionResult> GetPendingEvents([FromBody] PaginationDto paginationDto)
        {
            var result = await _eventsService.GetPendingEvents(paginationDto);
            return Ok(result);
        }

        [HttpPost("GetOrganisatorEvents")]
        [Authorize]
        public async Task<IActionResult> GetOrganisatorEvents([FromBody] PaginationDto paginationDto, string statusChoice)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _eventsService.GetOrganisatorEvents(paginationDto, userId, statusChoice);
            return Ok(result);
        }

        [HttpPut("UpdateEvent")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent(CreateEventDto createEventDto, long eventID)
        {
            if (!EventsEntityExists(eventID))
            {
                return NotFound();
            }
            var data = await _context.Events.FindAsync(eventID);
            if(data.Status != StatusEnum.Pending.ToString())
            {
                return BadRequest("Status nie jest pending");
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool EventsEntityExists(long id)
        {
            return (_context.Events?.Any(e => e.EventID == id)).GetValueOrDefault();
        }
    }
}
