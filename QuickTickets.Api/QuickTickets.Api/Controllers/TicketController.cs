using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure;
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
    public class TicketController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly EventsService _eventsService;
        private readonly AccountService _accountService;
        private readonly TicketService _ticketService;

        public TicketController(DataContext context, EventsService eventsService, AccountService accountService, TicketService ticketService)
        {
            _context = context;
            _eventsService = eventsService;
            _accountService = accountService;
            _ticketService = ticketService;
        }

        [HttpPost("GetMyTickets")]
        public async Task<ActionResult<IEnumerable<object>>> GetMyTickets([FromBody]PaginationDto paginationDto,bool choice)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");


            var result = await _ticketService.GetTicketsForUser(paginationDto, userId, choice);
            return Ok(result);
        }


        [HttpPost("GetMyTicket")]
        public async Task<IActionResult> GetMyTicket(long ticketID)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            if (_context.Tickets == null)
            {
                return NotFound();
            }
            if (!TicketEntityExists(ticketID))
            {
                return NotFound();
            }

            var data = await _context.Tickets.Include(x => x.Transaction).ThenInclude(y => y.User).Include(x => x.Event).ThenInclude(y => y.Owner).Include(x => x.Event).ThenInclude(y => y.Type).Include(x => x.Event).ThenInclude(y => y.Location).FirstOrDefaultAsync(x => x.TicketID == ticketID);
            var listOfTickets = new List<dynamic>();
            var response = new
            {
                TicketID = data.TicketID,
                Event = _eventsService.GetEventInfoDto(data.Event),
                NumberOfTickets = data.Amount,
                Cost = data.Transaction.Price,
                User = _accountService.GetUserInfoDto(data.Transaction.User),
            };
            return Ok(response);
        }


        private bool TicketEntityExists(long id)
        {
            return (_context.Tickets?.Any(e => e.TicketID == id)).GetValueOrDefault();
        }
    }
}
