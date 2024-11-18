using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;

namespace QuickTickets.Api.Services
{
    public interface ITrackUserMovesService
    {
        public Task<IActionResult> Add(AddUserEventHistoryRequestDto addUserEventHistoryRequestDto, Guid userId);
    }
}
