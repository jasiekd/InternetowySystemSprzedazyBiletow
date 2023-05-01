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
                return Unauthorized();
            else
                return Ok(result);
        }
        [HttpPost("loginWithGoogle")]
        [AllowAnonymous]
        public IActionResult LoginUserWithGoogle([FromBody] GoogleLoginRequest loginData)
        {
            var result = _accountService.LoginUserWithGoogle(loginData);
            if (result == null)
                return Unauthorized();
            else
                return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AccountEntity>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _context.Accounts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountEntity>> GetAccountEntity(Guid id)
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

            return accountEntity;
        }
        [HttpGet("getUser")]
        [Authorize]
        public async Task<IActionResult> GetAccount()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            AccountEntity accountEntity =  await _context.Accounts.FindAsync(userId);

            UserInfoDto temp = new UserInfoDto { 
                Name = accountEntity.Name,
                Surname = accountEntity.Surname,
            };

            return Ok(temp);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountEntity(Guid id, AccountEntity accountEntity)
        {
            if (id != accountEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountEntityExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AccountEntity>> PostAccountEntity(AccountEntity accountEntity)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'DataContext.Accounts'  is null.");
            }
            accountEntity.Id = Guid.NewGuid();
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(accountEntity.Password);
            byte[] hash = sha256.ComputeHash(bytes);
            accountEntity.Password = Convert.ToBase64String(hash);
            if (LoginEmailExists(accountEntity.Email, accountEntity.Login))
                return Problem("Account with that email or login already exist");
            _context.Accounts.Add(accountEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountEntity", new { id = accountEntity.Id }, accountEntity);
        }

        [HttpDelete("{id}")]
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

