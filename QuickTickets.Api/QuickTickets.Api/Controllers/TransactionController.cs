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
using Newtonsoft.Json;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TransactionService _transactionService;

        private const string clientId = "753756";
        private const string username = "bilety188@gmail.com";
        private const string password = "Biletybilety123$";
        private const string apiUrl = $"https://ssl.dotpay.pl/test_seller/api/v1/accounts/{clientId}/payment_links/";

        public TransactionController(DataContext context, TransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }
        public static string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                return hash;
            }
        }

        [HttpPost("Notify")]
        public async Task<IActionResult> Notify()
        {
            var formData = HttpContext.Request.Form;
            var transactionId = HttpContext.Request.Query["transactionId"];
            var status = formData["operation_status"];
            var number = formData["operation_number"];
            var transaction = await _context.Transactions.FindAsync(transactionId);
            transaction.DotPayID = number;
            transaction.DateUpdated = DateTime.Now;

            if(status == "rejected")
                transaction.Status = StatusEnum.Unpaid.ToString();
            else if(status == "completed")
            {
                transaction.Status = StatusEnum.Paid.ToString();
                var ticket = await _context.Tickets.Where(x => x.TransactionID == transactionId).FirstOrDefaultAsync();
                ticket.IsActive = true;
                _context.Tickets.Update(ticket);
            }

            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
            return Ok("OK");
        }

        [HttpPost("CreateTransaction")]
        [Authorize]
        public async Task<IActionResult> CreateTransaction([FromBody]TransactionRequestDto transactionRequestDto)
        {
            //Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            TransactionEntity transaction = new TransactionEntity
            {
                TransactionID = Guid.NewGuid(),
                UserId = userId,
                Price = transactionRequestDto.Cost,
            };

            var user = await _context.Accounts.FindAsync(userId);

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
                    url = $"http://localhost:3000/buy-ticket/{transaction.TransactionID}/",
                    urlc = $"https://r15lg05v-7235.euw.devtunnels.ms/api/Transaction/Notify?transactionId={transaction.TransactionID}",
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

                    var DotpayPin = "hgX5Sz100itQogpXX4V31iXzvDy1fRYA";


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
                    
                    return Ok(payment_link);

                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetPendingTransactions")]
        [AdminAuthorize]
        public async Task<IActionResult> GetPendingTransactions([FromBody]PaginationDto paginationDto)
        {
            if (_context.Transactions == null)
            {
                return NotFound();
            }
            var transactions = await _transactionService.GetTransactionsForAdmin(paginationDto);
            return transactions;
        }


        [HttpPut("AcceptTransaction")]
        [AdminAuthorize]
        
        public async Task<IActionResult> AcceptTransaction(Guid transactionID)
        {
            if (_context.Transactions == null)
            {
                return NotFound();
            }
            if(!TransactionEntityExists(transactionID))
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(transactionID);
            if (transaction.Status != StatusEnum.Pending.ToString())
                return BadRequest();
            transaction.Status = StatusEnum.AdminOffPaid.ToString();
            transaction.DateUpdated = DateTime.Now;
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("CancelTransaction")]
        [AdminAuthorize]
        public async Task<IActionResult> CancelTransaction(Guid transactionID)
        {
            if (_context.Transactions == null)
            {
                return NotFound();
            }
            if (!TransactionEntityExists(transactionID))
            {
                return NotFound();
            }
            var transaction = await _context.Transactions.FindAsync(transactionID);
            if (transaction.Status != StatusEnum.Pending.ToString())
                return BadRequest();
            transaction.Status = StatusEnum.Cancelled.ToString();
            transaction.DateUpdated = DateTime.Now;
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TransactionEntityExists(Guid id)
        {
            return (_context.Transactions?.Any(e => e.TransactionID == id)).GetValueOrDefault();
        }
    }
}