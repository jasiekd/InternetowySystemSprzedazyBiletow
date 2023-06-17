using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class TicketService
    {
        private readonly DataContext _context;
        private readonly EventsService _eventsService;
        public TicketService(DataContext context, EventsService eventsService)
        {
            _context = context;
            _eventsService = eventsService;
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




    }
}
