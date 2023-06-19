using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace QuickTickets.Api.Services
{

        public class TransactionService : ITransactionService
        {
            private readonly AccountService _accountService;
            private readonly DataContext _context;
            private const string tunnelLink = "https://zrtc29r2-7235.euw.devtunnels.ms";
            private const string clientId = "753756";
            private const string username = "bilety188@gmail.com";
            private const string password = "Biletybilety123$";
            private const string apiUrl = $"https://ssl.dotpay.pl/test_seller/api/v1/accounts/{clientId}/payment_links/";
            private const string DotpayPin = "hgX5Sz100itQogpXX4V31iXzvDy1fRYA";

        public TransactionService(AccountService accountService, DataContext context)
            {
                _accountService = accountService;
                _context = context;
            }

        public string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                return hash;
            }
        }

        public async Task<string> CreatePaidLink(Guid userID, TransactionRequestDto transactionRequestDto)
        {
            TransactionEntity transaction = new TransactionEntity
            {
                TransactionID = Guid.NewGuid(),
                UserId = userID,
                Price = transactionRequestDto.Cost,
            };

            var user = await _context.Accounts.FindAsync(userID);

            try
            {
                var requestData = new
                {
                    amount = transactionRequestDto.Cost,
                    currency = "PLN",
                    description = transactionRequestDto.Desc,
                    control = transaction.TransactionID.ToString(),
                    language = "pl",
                    ignore_last_payment_channel = 1,
                    redirection_type = 0,
                    url = $"{tunnelLink}/api/Transaction/dotpay-redirect?transactionId={transaction.TransactionID}",
                    urlc = $"{tunnelLink}/api/Transaction/Notify?transactionId={transaction.TransactionID}",
                    payer = new
                    {
                        first_name = user.Name,
                        last_name = user.Surname,
                        email = user.Email,
                    },
                    seller = new
                    {
                        p_info = "Quick Tickets"
                    }
                };


                var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

                    var response = await client.PostAsync(apiUrl, requestContent);

                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();

                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                    string payment_url = responseObject.payment_url;
                    string pid = responseObject.token;

                    var chkchain = DotpayPin + pid;

                    var chk = Hash(chkchain);


                    var payment_link = "https://ssl.dotpay.pl/test_payment/?chk=" + chk + "&pid=" + pid;

                    transaction.DotPayID = pid;

                    var ticket = new TicketEntity
                    {
                        TicketID = 0,
                        EventID = transactionRequestDto.EventID,
                        Amount = transactionRequestDto.NumberOfTickets,
                        TransactionID = transaction.TransactionID
                    };



                    _context.Transactions.Add(transaction);
                    _context.Tickets.Add(ticket);
                    await _context.SaveChangesAsync();

                    return payment_link;
                }
            }
            catch (Exception ex)
            {
                return ((int)HttpStatusCode.InternalServerError, ex.Message).ToString();
            }
        }

        public async Task<IActionResult> GetTransactionsForAdmin(PaginationDto paginationDto)
        {
            try
            {
                var data = _context.Transactions.AsQueryable().Include(x => x.User).OrderBy(x => x.DateCreated);


                return await GetPaginatedTransactions(paginationDto, data);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetPendingTransactionsForAdmin(PaginationDto paginationDto)
            {
                try
                {
                    var data = _context.Transactions.AsQueryable().Include(x => x.User).Where(x => x.Status == StatusEnum.Pending.ToString()).OrderBy(x => x.DateCreated);


                return await GetPaginatedTransactions(paginationDto, data);

                }
                catch (Exception ex)
                {
                    throw;
                }
            }


            private async Task<IActionResult> GetPaginatedTransactions(PaginationDto paginationDto, IQueryable<TransactionEntity> data)
            {
                var totalCount = await data.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

                data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

                var transactionList = new List<dynamic>();

                foreach (var temp in await data.ToListAsync())
                {
                transactionList.Add(new
                    {
                        TransactionID = temp.TransactionID,
                        User = _accountService.GetUserInfoDto(temp.User),
                        Price = temp.Price,
                        TransactionDate = temp.DateCreated,
                        
                    });
                }

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageIndex = paginationDto.pageIndex,
                    PageSize = paginationDto.pageSize,
                    Transactions = transactionList
                };
                return new OkObjectResult(result);
            }

            public async Task DotPayTrans(Guid transactionId, string number, string status)
            {
                
                var transaction = await _context.Transactions.FindAsync(transactionId);
                transaction.DotPayID = number;
                transaction.DateUpdated = DateTime.Now;

                if (status == "rejected")
                    transaction.Status = StatusEnum.Unpaid.ToString();
                else if (status == "completed")
                {
                    transaction.Status = StatusEnum.Paid.ToString();
                    var ticket = await _context.Tickets.Where(x => x.TransactionID == transactionId).FirstOrDefaultAsync();
                    ticket.IsActive = true;
                    _context.Tickets.Update(ticket);
                }

                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }

            public async Task<IActionResult> GetStatusTransaction(Guid transactionID, Guid userId)
            {
                var transaction = await _context.Transactions.FindAsync(transactionID);

                if (transaction.UserId != userId)
                {
                    return new UnauthorizedResult();
                }

                var response = new
                {
                    transactionID = transaction.TransactionID,
                    transactionStatus = transaction.Status,
                };
            return new OkObjectResult(response);
            }
            public async Task<IActionResult> GetPendingTransactions(PaginationDto paginationDto)
            {
                if (_context.Transactions == null)
                {
                    return new NotFoundResult();
                }
                var transactions = await GetPendingTransactionsForAdmin(paginationDto);
                return transactions;
            }

            public async Task<IActionResult> GetAllTransactions([FromBody] PaginationDto paginationDto)
            {
                if (_context.Transactions == null)
                {
                    return new NotFoundResult();
                }
                var transactions = await GetTransactionsForAdmin(paginationDto);
                return transactions;
            }
            public async Task<IActionResult> AcceptTransaction(Guid transactionID)
            {
                if (_context.Transactions == null)
                {
                    return new NotFoundResult();
                }
                if (!TransactionEntityExists(transactionID))
                {
                    return new NotFoundResult();
            }

                var transaction = await _context.Transactions.FindAsync(transactionID);
                if (transaction.Status != StatusEnum.Pending.ToString())
                    return new BadRequestResult();
                transaction.Status = StatusEnum.AdminOffPaid.ToString();
                transaction.DateUpdated = DateTime.Now;
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            public async Task<IActionResult> CancelTransaction(Guid transactionID)
            {
                if (_context.Transactions == null)
                {
                    return new NotFoundResult();
                }
                if (!TransactionEntityExists(transactionID))
                {
                    return new NotFoundResult();
                }

                var transaction = await _context.Transactions.FindAsync(transactionID);
                if (transaction.Status != StatusEnum.Pending.ToString())
                    return new BadRequestResult();
                transaction.Status = StatusEnum.Cancelled.ToString();
                transaction.DateUpdated = DateTime.Now;
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();

                return new OkResult();
            }


        private bool TransactionEntityExists(Guid id)
        {
            return (_context.Transactions?.Any(e => e.TransactionID == id)).GetValueOrDefault();
        }
    }
}

