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

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly DataContext _context;

        public AccountController(AccountService accountService, DataContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginUser([FromBody] UserLoginRequestDto loginData)
        {
            var result = _accountService.LoginUser(loginData);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
        [HttpPost("loginWithGoogle")]
        [AllowAnonymous]
        public IActionResult LoginUserWithGoogle([FromBody] GoogleLoginRequestDto loginData)
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

            AccountEntity accountEntity =  await _context.Accounts.FindAsync(userId);

            UserInfoDto temp = _accountService.GetUserInfoDto(accountEntity);

            return Ok(temp);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateAccount")]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(RegisterInfoDto registerInfoDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");
            AccountEntity accountEntity = await _context.Accounts.FindAsync(userId);
            if (LoginEmailExists(registerInfoDto.Email, registerInfoDto.Login) && registerInfoDto.Email != accountEntity.Email && registerInfoDto.Login != accountEntity.Login)
            {
                return BadRequest("User istnieje!");
            }
            
            
            accountEntity.Surname = registerInfoDto.Surname;
            accountEntity.Name = registerInfoDto.Name;
            accountEntity.Login = registerInfoDto.Login;
            accountEntity.DateOfBirth = registerInfoDto.DateOfBirth;
            accountEntity.Password = _accountService.HashPassword(registerInfoDto.Password);
            accountEntity.Email = registerInfoDto.Email;

            try
            {
                _context.Accounts.Update(accountEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountEntityExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterInfoDto>> PostAccountEntity(RegisterInfoDto registerInfoDto)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'DataContext.Accounts'  is null.");
            }
            AccountEntity accountEntity = new AccountEntity
            {
                Id = Guid.NewGuid(),
                Name = registerInfoDto.Name,
                Surname = registerInfoDto.Surname,
                Login = registerInfoDto.Login,
                Email = registerInfoDto.Email,
                Password = _accountService.HashPassword(registerInfoDto.Password),
                DateOfBirth = registerInfoDto.DateOfBirth,
                RoleID = 2
            };

            if (LoginEmailExists(accountEntity.Email, accountEntity.Login))
                return Problem("Account with that email or login already exist");
            _context.Accounts.Add(accountEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountEntity", new { id = accountEntity.Id }, accountEntity);
        }

        [HttpDelete("{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeleteAccountEntity(Guid id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var accountEntity = await _context.Accounts.FindAsync(id);
            if (accountEntity == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accountEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountEntityExists(Guid id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool LoginEmailExists(string email, string login) 
        {
            return (_context.Accounts?.Any(e => e.Email == email || e.Login==login)).GetValueOrDefault();
        }
    }
}

