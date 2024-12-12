using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using System;
using System.Threading.Tasks;

namespace QuickTickets.Api.Services
{
    public interface ITicketService
    {
        Task<ActionResult<IEnumerable<object>>> GetMyTickets([FromBody] PaginationDto paginationDto, Guid userId, bool choice);
        Task<IActionResult> GetMyTicket(long ticketID);
        Task<IActionResult> GetMyTicketForTransactionID(Guid transactionID);
        Task<IQueryable<TicketEntity>> GetTicketsForUserFromDb(Guid userId, bool isActive);
    }
}
