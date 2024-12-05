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
        private const string DotpayPin = "hgX5Sz100itQogpXX4V31iXzvDy1fRYA";
        private readonly string _ipAddress;

        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService, IConfiguration configuration)
        {
            _transactionService = transactionService;
            _ipAddress = configuration["AppSettings:IpAddress"];
        }

        [HttpPost("Notify")]
        public async Task<IActionResult> Notify()
        {
            var formData = HttpContext.Request.Form;
            StringBuilder signBuilder = new StringBuilder();
            signBuilder.Append(DotpayPin)
                       .Append(formData["id"])
                       .Append(formData["operation_number"])
                       .Append(formData["operation_type"])
                       .Append(formData["operation_status"])
                       .Append(formData["operation_amount"])
                       .Append(formData["operation_currency"])
                       .Append(formData["operation_withdrawal_amount"])
                       .Append(formData["operation_commission_amount"])
                       .Append(formData["is_completed"])
                       .Append(formData["operation_original_amount"])
                       .Append(formData["operation_original_currency"])
                       .Append(formData["operation_datetime"])
                       .Append(formData["operation_related_number"])
                       .Append(formData["control"])
                       .Append(formData["description"])
                       .Append(formData["email"])
                       .Append(formData["p_info"])
                       .Append(formData["p_email"])
                       .Append(formData["credit_card_issuer_identification_number"])
                       .Append(formData["credit_card_masked_number"])
                       .Append(formData["credit_card_expiration_year"])
                       .Append(formData["credit_card_expiration_month"])
                       .Append(formData["credit_card_brand_codename"])
                       .Append(formData["credit_card_brand_code"])
                       .Append(formData["credit_card_unique_identifier"])
                       .Append(formData["credit_card_id"])
                       .Append(formData["channel"])
                       .Append(formData["channel_country"])
                       .Append(formData["geoip_country"])
                       .Append(formData["payer_bank_account_name"])
                       .Append(formData["payer_bank_account"])
                       .Append(formData["payer_transfer_title"])
                       .Append(formData["blik_voucher_pin"])
                       .Append(formData["blik_voucher_amount"])
                       .Append(formData["blik_voucher_amount_used"])
                       .Append(formData["channel_reference_id"])
                       .Append(formData["operation_seller_code"]);


            string signature = _transactionService.Hash(signBuilder.ToString());

            if(signature == formData["signature"])
            {
                Guid transactionId = Guid.Parse(HttpContext.Request.Query["transactionId"]);
                var status = formData["operation_status"];
                var number = formData["operation_number"];
                await _transactionService.DotPayTrans(transactionId, number, status);
            }

            return Ok("OK");
        }

        [HttpPost("dotpay-redirect")]
        public IActionResult DotpayRedirect()
        {
            // Przekieruj użytkownika do docelowej strony frontendowej
            var transactionId = HttpContext.Request.Query["transactionId"];
            return Redirect($"{_ipAddress}/buy-ticket/{transactionId}/");
        }

        [HttpPost("CreateTransaction")]
        [Authorize]
        public async Task<IActionResult> CreateTransaction([FromBody]TransactionRequestDto transactionRequestDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            return Ok(await _transactionService.CreatePaidLink(userId, transactionRequestDto));
        }

        [HttpPost("GetStatusTransaction")]
        [Authorize]
        public async Task<IActionResult> GetStatusTransaction([FromBody] Guid transactionID)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            return await _transactionService.GetStatusTransaction(transactionID, userId);
        }

        [HttpPost("GetPendingTransactions")]
        [AdminAuthorize]
        public async Task<IActionResult> GetPendingTransactions([FromBody]PaginationDto paginationDto)
        {
            return await _transactionService.GetPendingTransactions(paginationDto);
        }

        [HttpPost("GetAllTransactions")]
        [AdminAuthorize]
        public async Task<IActionResult> GetAllTransactions([FromBody] PaginationDto paginationDto)
        {
            return await _transactionService.GetAllTransactions(paginationDto);
        }

        [HttpPut("AcceptTransaction")]
        [AdminAuthorize]
        
        public async Task<IActionResult> AcceptTransaction(Guid transactionID)
        {
            return await _transactionService.AcceptTransaction(transactionID);
        }

        [HttpPut("CancelTransaction")]
        [AdminAuthorize]
        public async Task<IActionResult> CancelTransaction(Guid transactionID)
        {
            return await _transactionService.CancelTransaction(transactionID);
        }

    }
}