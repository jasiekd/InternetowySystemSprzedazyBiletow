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

        public TicketController(DataContext context, EventsService eventsService, AccountService accountService)
        {
            _context = context;
            _eventsService = eventsService;
            _accountService = accountService;
        }

        [HttpPost("GetMyTickets")]
        public async Task<ActionResult<IEnumerable<object>>> GetMyTickets(bool choice)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");
            
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            
            var data = await _context.Tickets.Include(x => x.Transaction).Include(x => x.Event).ThenInclude(y => y.Owner).Include(x => x.Event).ThenInclude(y => y.Type).Include(x => x.Event).ThenInclude(y => y.Location).Where(x => x.Transaction.UserId == userId && x.IsActive == choice).ToListAsync();
            var listOfTickets = new List<dynamic>();
            foreach( var temp in data )
            {
                listOfTickets.Add(new
                {
                    TicketID = temp.TicketID,
                    Event = _eventsService.GetEventInfoDto(temp.Event),
                    NumberOfTickets = temp.Amount,
                    Cost = temp.Transaction.Price,
                });
            }
            return Ok(listOfTickets);
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



        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketEntity>> GetTicketEntity(long id)
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            var ticketEntity = await _context.Tickets.FindAsync(id);

            if (ticketEntity == null)
            {
                return NotFound();
            }

            return ticketEntity;
        }

        // PUT: api/Ticket/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketEntity(long id, TicketEntity ticketEntity)
        {
            if (id != ticketEntity.TicketID)
            {
                return BadRequest();
            }

            _context.Entry(ticketEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ticket
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketEntity>> PostTicketEntity(TicketEntity ticketEntity)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'DataContext.Tickets'  is null.");
          }
            _context.Tickets.Add(ticketEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketEntity", new { id = ticketEntity.TicketID }, ticketEntity);
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketEntity(long id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticketEntity = await _context.Tickets.FindAsync(id);
            if (ticketEntity == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticketEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketEntityExists(long id)
        {
            return (_context.Tickets?.Any(e => e.TicketID == id)).GetValueOrDefault();
        }
    }
}
