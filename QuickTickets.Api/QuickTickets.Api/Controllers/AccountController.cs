using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestDto loginData)
        {
            var result = _accountService.LoginUser(loginData);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
        [HttpPost("loginWithGoogle")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUserWithGoogle([FromBody] GoogleLoginRequestDto loginData)
        {
            var result = _accountService.LoginUserWithGoogle(loginData);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("getUser")]
        [Authorize]
        public async Task<IActionResult> GetAccount()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            UserInfoDto accountEntity = await _accountService.GetAccount(userId);

            return Ok(accountEntity);
        }

        [HttpPut("UpdateAccount")]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(RegisterInfoDto registerInfoDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");

            return await _accountService.Update(registerInfoDto, userId);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAccountEntity(RegisterInfoDto registerInfoDto)
        {

            return await _accountService.Register(registerInfoDto);
        }

        [HttpPut("DeleteAccount/{accountID}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeleteAccount(Guid accountID)
        {

            return await _accountService.Delete(accountID);
        }

        [HttpPost("GetListOfUsers")]
        [AdminAuthorize]
        public async Task<IActionResult> GetListOfUsers([FromBody]PaginationDto paginationDto)
        {
            return await _accountService.GetListOfUsers(paginationDto);
        }

    }
}

