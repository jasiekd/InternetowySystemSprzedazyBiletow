using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserApplicationController : ControllerBase
    {
        private readonly IOrganiserApplicationService _organiserApplicationService;

        public OrganiserApplicationController(IOrganiserApplicationService organiserApplicationService)
        {
            _organiserApplicationService = organiserApplicationService;
        }

        [HttpPost("SendOrganiserApplication")]
        [Authorize]
        public async Task<IActionResult> SendOrganiserApplication([FromBody]OrganiserApplicationDto organiserApp)
        {
            return await _organiserApplicationService.SendOrganiserApplication(organiserApp);
        }

        [HttpPost("GetPendingOrganiserApplications")]
        [AdminAuthorize]
        public async Task<IActionResult> GetPendingOrganiserApplications([FromBody] PaginationDto paginationDto)
        {
            return await _organiserApplicationService.GetPendingOrganiserApplications(paginationDto);
        }

        [HttpPost("AcceptOrganiserApplication")]
        [AdminAuthorize]
        public async Task<IActionResult> AcceptOrganiserApplication([FromBody]long id)
        {
            return await _organiserApplicationService.AcceptOrganiserApplication(id);
        }
        [HttpPost("CancelOrganiserApplication")]
        [AdminAuthorize]
        public async Task<IActionResult> CancelOrganiserApplication([FromBody] long id)
        {
            return await _organiserApplicationService.CancelOrganiserApplication(id);
        }

    }
}
