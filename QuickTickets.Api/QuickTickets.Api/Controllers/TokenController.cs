using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public IActionResult RefreshBearer([FromBody] TokenInfoDto tokenData)
        {
            var result = _tokenService.RefreshBearerToken(tokenData);
            if (result == null)
                return Unauthorized();
            else
                return Ok(result);
        }
    }
}
