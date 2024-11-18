using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventHistoryController : ControllerBase
    {
        private readonly IUserEventHistoryService _userEventHistoryService;

        public UserEventHistoryController(IUserEventHistoryService userEventHistoryService)
        {
            _userEventHistoryService = userEventHistoryService;
        }

        //[HttpPost("Add")]
        //[Authorize]
        //public async Task<IActionResult> Add([FromBody] AddUserEventHistoryRequestDto addUserEventHistoryRequestDto)
        //{
        //    Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

        //    return await _userEventHistoryService.Add(addUserEventHistoryRequestDto, userId);
        //}
        [HttpGet("GetPredictedEvents")]
        [Authorize]
        public async Task<IActionResult> GetPredictedEvents()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            return await _userEventHistoryService.GetPredictedEvents(userId);
        }
    }
}
