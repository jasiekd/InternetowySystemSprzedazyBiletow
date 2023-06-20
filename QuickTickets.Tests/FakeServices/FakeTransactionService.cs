using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTickets.Tests.FakeServices
{
    public class FakeTransactionService : ITransactionService
    {
        public async Task<IActionResult> GetStatusTransaction(Guid userId, [FromBody] Guid transactionID)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> GetPendingTransactions([FromBody] PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> GetAllTransactions([FromBody] PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> AcceptTransaction(Guid transactionID)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> CancelTransaction(Guid transactionID)
        {
            throw new NotImplementedException();
        }
        public async Task DotPayTrans(Guid transactionId, string number, string status)
        {
            throw new NotImplementedException();
        }
        public async Task<string> CreatePaidLink(Guid userID, TransactionRequestDto transactionRequestDto)
        {
            return "https://ssl.dotpay.pl/test_payment/?pid=no68ellkfim0xoppj3212ygimgyfz7r1";
        }
        public string Hash(string input)
        {
            throw new NotImplementedException();
        }
    }
}
