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
    [Authorize]
    public class TicketController : ControllerBase
    {

        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("GetMyTickets")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<object>>> GetMyTickets([FromBody]PaginationDto paginationDto,bool choice)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            return await _ticketService.GetMyTickets(paginationDto, userId, choice);
        }


        [HttpPost("GetMyTicket")]
        [Authorize]
        public async Task<IActionResult> GetMyTicket(long ticketID)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            return await _ticketService.GetMyTicket(ticketID);
        }

        [HttpPost("GetMyTicketForTransactionID")]
        [Authorize]
        public async Task<IActionResult> GetMyTicketForTransactionID(Guid transactionID)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            return await _ticketService.GetMyTicketForTransactionID(transactionID);
        }

    }
}
