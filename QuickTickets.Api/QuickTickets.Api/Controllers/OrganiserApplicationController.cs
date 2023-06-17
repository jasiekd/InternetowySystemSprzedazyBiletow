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
        private readonly DataContext _context;
        private readonly OrganiserApplicationService _organiserApplicationService;

        public OrganiserApplicationController(DataContext context, OrganiserApplicationService organiserApplicationService)
        {
            _context = context;
            _organiserApplicationService = organiserApplicationService;
        }



        [HttpPost("SendOrganiserApplication")]
        [Authorize]
        public async Task<IActionResult> SendOrganiserApplication([FromBody]OrganiserApplicationDto organiserApp)
        {
            var application = new OrganiserApplicationEntity
            {
                Id = 0,
                UserId = organiserApp.UserId,
                Description = organiserApp.Description,
            };
            _context.OrganisersApplications.Add(application);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("GetPendingOrganiserApplications")]
        [AdminAuthorize]
        public async Task<IActionResult> GetPendingOrganiserApplications([FromBody] PaginationDto paginationDto)
        {
            var result = await _organiserApplicationService.GetPendingOrganiserApplications(paginationDto);
            return Ok(result);
        }


        [HttpPost("AcceptOrganiserApplication")]
        [AdminAuthorize]
        public async Task<IActionResult> AcceptOrganiserApplication([FromBody]long id)
        {

            OrganiserApplicationEntity organiserApplication = await _context.OrganisersApplications.FindAsync(id);
            AccountEntity accountEntity = await _context.Accounts.FindAsync(organiserApplication.UserId);

            if (organiserApplication == null)
            {
                return NotFound();
            }
            organiserApplication.Status = StatusEnum.Confirmed.ToString();
            organiserApplication.DateModified = DateTime.Now;
            accountEntity.RoleID = 3;
            
            _context.OrganisersApplications.Update(organiserApplication);
            _context.Accounts.Update(accountEntity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("CancelOrganiserApplication")]
        [AdminAuthorize]
        public async Task<IActionResult> CancelOrganiserApplication([FromBody] long id)
        {

            OrganiserApplicationEntity organiserApplication = await _context.OrganisersApplications.FindAsync(id);

            if (organiserApplication == null)
            {
                return NotFound();
            }
            organiserApplication.Status = StatusEnum.Cancelled.ToString();
            organiserApplication.DateModified = DateTime.Now;

            _context.OrganisersApplications.Update(organiserApplication);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
