using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public interface IEventsService
    {
        public Task<IActionResult> PostEventEntity(CreateEventDto createEventDto, Guid userId);
        public Task<ActionResult<EventInfoDto>> GetEvent(long id, Guid userId);
        public Task<ActionResult<IEnumerable<EventInfoDto>>> getHotEvents();
        public Task<ActionResult<IEnumerable<LocationsEntity>>> getHotLocations();
        public Task<IActionResult> SearchEvents(SearchEventDto searchEventDto);
        public Task<IActionResult> AcceptEvent(long id);
        public Task<IActionResult> CancelEvent(long id);
        public Task<IActionResult> GetPendingEventsAction(PaginationDto paginationDto);
        public Task<IActionResult> GetOrganisatorEventsAction(PaginationDto paginationDto, string statusChoice, Guid userId);
        public Task<IActionResult> UpdateEvent(CreateEventDto createEventDto, long eventID);
        public EventInfoDto GetEventInfoDto(EventsEntity eventsEntity);
        public Task<IActionResult> CanBuyTicket(long eventID);
        public Task<IEnumerable<EventInfoDto>> getHotEventsInfo();
    }
}
