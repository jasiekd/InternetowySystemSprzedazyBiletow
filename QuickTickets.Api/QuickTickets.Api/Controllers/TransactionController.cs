using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuickTickets.Api.Data;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using static System.Net.Mime.MediaTypeNames;

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


        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction(double cost, string desc)
        {
            //Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            TransactionEntity transaction = new TransactionEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var user = await _context.Accounts.FindAsync(userId);

            try
            {
                var requestData = new
                {
                    amount = cost,
                    currency = "PLN",
                    description = desc,
                    control = transaction.Id.ToString(),
                    language = "pl",
                    ignore_last_payment_channel = 1,
                    redirection_type = 0,
                    url = $"http://localhost:3000/buy-ticket/{transaction.Id}/",
                    urlc = "http://localhost:3000/",
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



                    return Ok(payment_link);

                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                return StatusCode(500, ex.Message);
            }

        }







        // GET: api/Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionEntity>>> GetTransactions()
        {
          if (_context.Transactions == null)
          {
              return NotFound();
          }
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionEntity>> GetTransactionEntity(Guid id)
        {
          if (_context.Transactions == null)
          {
              return NotFound();
          }
            var transactionEntity = await _context.Transactions.FindAsync(id);

            if (transactionEntity == null)
            {
                return NotFound();
            }

            return transactionEntity;
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionEntity(Guid id, TransactionEntity transactionEntity)
        {
            if (id != transactionEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(transactionEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionEntityExists(id))
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

        // POST: api/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionEntity>> PostTransactionEntity(TransactionEntity transactionEntity)
        {
          if (_context.Transactions == null)
          {
              return Problem("Entity set 'DataContext.Transactions'  is null.");
          }
            _context.Transactions.Add(transactionEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionEntity", new { id = transactionEntity.Id }, transactionEntity);
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionEntity(Guid id)
        {
            if (_context.Transactions == null)
            {
                return NotFound();
            }
            var transactionEntity = await _context.Transactions.FindAsync(id);
            if (transactionEntity == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactionEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionEntityExists(Guid id)
        {
            return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
