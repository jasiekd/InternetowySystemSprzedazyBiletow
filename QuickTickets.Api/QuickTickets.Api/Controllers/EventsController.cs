using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpPost("addEvent")]
        [Authorize]
        public async Task<IActionResult> PostEventEntity([FromBody] CreateEventDto createEventDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            return await _eventsService.PostEventEntity(createEventDto,userId);
        }

        [HttpGet("getEvent/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EventInfoDto>> GetEvent(long id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid userId = userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
            var eventInfo = await _eventsService.GetEvent(id, userId);

            if (eventInfo == null)
            {
                return NotFound("Event not found.");
            }

            return Ok(eventInfo.Result);
        }

        [HttpGet("getHotEvents")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EventInfoDto>>> getHotEvents()
        {
            return await _eventsService.getHotEvents();
        }

        [HttpGet("getHotLocations")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LocationsEntity>>> getHotLocations()
        {
            return await _eventsService.getHotLocations();
        }

        [HttpPost("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchEvents([FromBody]SearchEventDto searchEventDto)
        {
            return await _eventsService.SearchEvents(searchEventDto);
        }

        [HttpPost("AcceptEvent")]
        [AdminAuthorize]
        public async Task<IActionResult> AcceptEvent([FromBody] long id)
        {
            return await _eventsService.AcceptEvent(id);
        }
        [HttpPost("CancelEvent")]
        [AdminAuthorize]
        public async Task<IActionResult> CancelEvent([FromBody] long id)
        {
            return await _eventsService.CancelEvent(id);
        }


        [HttpPost("GetPendingEvents")]
        [AdminAuthorize]
        public async Task<IActionResult> GetPendingEvents([FromBody] PaginationDto paginationDto)
        {
            return await _eventsService.GetPendingEventsAction(paginationDto);
        }

        [HttpPost("GetOrganisatorEvents")]
        [Authorize]
        public async Task<IActionResult> GetOrganisatorEvents([FromBody] PaginationDto paginationDto, string statusChoice)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _eventsService.GetOrganisatorEventsAction(paginationDto,statusChoice,userId);
        }

        [HttpPut("UpdateEvent")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent(CreateEventDto createEventDto, long eventID)
        {
            return await _eventsService.UpdateEvent(createEventDto, eventID);
        }
        [HttpPost("CanBuyTicket")]
        [Authorize]
        public async Task<IActionResult> CanBuyTicket(long eventID)
        {
            return await _eventsService.CanBuyTicket(eventID);
        }
    }
}
