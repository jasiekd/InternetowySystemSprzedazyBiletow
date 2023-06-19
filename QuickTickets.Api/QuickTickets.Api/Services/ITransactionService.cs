using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using System;
using System.Threading.Tasks;

namespace QuickTickets.Api.Services
{
    public interface ITransactionService
    {
        Task<IActionResult> GetStatusTransaction(Guid userId, [FromBody] Guid transactionID);
        Task<IActionResult> GetPendingTransactions([FromBody] PaginationDto paginationDto);
        Task<IActionResult> GetAllTransactions([FromBody] PaginationDto paginationDto);
        Task<IActionResult> AcceptTransaction(Guid transactionID);
        Task<IActionResult> CancelTransaction(Guid transactionID);
        Task DotPayTrans(Guid transactionId, string number, string status);
        Task<string> CreatePaidLink(Guid userID, TransactionRequestDto transactionRequestDto);
        string Hash(string input);
    }
}