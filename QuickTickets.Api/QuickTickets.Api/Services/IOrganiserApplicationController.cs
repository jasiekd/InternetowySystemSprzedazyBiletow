using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using System.Threading.Tasks;

namespace QuickTickets.Api.Services
{
    public interface IOrganiserApplicationService
    { 
        Task<IActionResult> SendOrganiserApplication([FromBody] OrganiserApplicationDto organiserApp);
        Task<IActionResult> GetPendingOrganiserApplications([FromBody] PaginationDto paginationDto);
        Task<IActionResult> AcceptOrganiserApplication([FromBody] long id);
        Task<IActionResult> CancelOrganiserApplication([FromBody] long id);
    }
}