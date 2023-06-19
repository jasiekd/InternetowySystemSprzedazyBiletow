using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using System.Security.Claims;

namespace QuickTickets.Api.Services
{
    public class TicketService : ITicketService
    {
        private readonly DataContext _context;
        private readonly IEventsService _eventsService;
        private readonly IAccountService _accountService;
        public TicketService(DataContext context, IEventsService eventsService, IAccountService accountService)
        {
            _context = context;
            _eventsService = eventsService;
            _accountService = accountService;
        }


        public async Task<IActionResult> GetTicketsForUser(PaginationDto paginationDto, Guid userId, bool choice)
        {
            try
            {
                var data = _context.Tickets.AsQueryable().Include(e => e.Transaction).Include(x => x.Event).ThenInclude(y => y.Owner).Include(x => x.Event).ThenInclude(y => y.Type).Include(x => x.Event).ThenInclude(y => y.Location).Where(e => e.IsActive == choice && e.Transaction.UserId == userId);

                var totalCount = await data.CountAsync();
                return await GetPaginatedTickets(paginationDto, data);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetPaginatedTickets(PaginationDto paginationDto, IQueryable<TicketEntity> data)
        {

            var totalCount = await data.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

            data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

            var ticketList = new List<dynamic>();

            foreach (var temp in await data.ToListAsync())
            {
                ticketList.Add(new
                {
                    TicketID = temp.TicketID,
                    Event = _eventsService.GetEventInfoDto(temp.Event),
                    NumberOfTickets = temp.Amount,
                    Cost = temp.Transaction.Price,
                });
            }

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageIndex = paginationDto.pageIndex,
                PageSize = paginationDto.pageSize,
                Tickets = ticketList
            };
            return new OkObjectResult(result);
        }



        public async Task<ActionResult<IEnumerable<object>>> GetMyTickets([FromBody] PaginationDto paginationDto, Guid userId, bool choice)
        {
            var result = await GetTicketsForUser(paginationDto, userId, choice);
            return new OkObjectResult(result);
        }


        public async Task<IActionResult> GetMyTicket(long ticketID)
        {

            if (_context.Tickets == null)
            {
                return new NotFoundResult();
            }
            if (!TicketEntityExists(ticketID))
            {
                return new NotFoundResult();
            }

            var data = await _context.Tickets.Include(x => x.Transaction).ThenInclude(y => y.User).Include(x => x.Event).ThenInclude(y => y.Owner).Include(x => x.Event).ThenInclude(y => y.Type).Include(x => x.Event).ThenInclude(y => y.Location).FirstOrDefaultAsync(x => x.TicketID == ticketID);
            var response = new
            {
                TicketID = data.TicketID,
                Event = _eventsService.GetEventInfoDto(data.Event),
                NumberOfTickets = data.Amount,
                Cost = data.Transaction.Price,
                User = _accountService.GetUserInfoDto(data.Transaction.User),
            };
            return new OkObjectResult(response);
        }


        public async Task<IActionResult> GetMyTicketForTransactionID(Guid transactionID)
        {
            if (_context.Tickets == null)
            {
                return new NotFoundResult();
            }

            var data = await _context.Tickets.Include(x => x.Transaction).ThenInclude(y => y.User).Include(x => x.Event).ThenInclude(y => y.Owner).Include(x => x.Event).ThenInclude(y => y.Type).Include(x => x.Event).ThenInclude(y => y.Location).FirstOrDefaultAsync(x => x.TransactionID == transactionID);
            var response = new
            {
                TicketID = data.TicketID,
                Event = _eventsService.GetEventInfoDto(data.Event),
                NumberOfTickets = data.Amount,
                Cost = data.Transaction.Price,
                User = _accountService.GetUserInfoDto(data.Transaction.User),
            };
            return new OkObjectResult(response);
        }

        private bool TicketEntityExists(long id)
        {
            return (_context.Tickets?.Any(e => e.TicketID == id)).GetValueOrDefault();
        }


    }
}

