using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;

namespace QuickTickets.Api.Services
{
    public interface ITokenService
    {
        TokenInfoDto RefreshBearerToken(TokenInfoDto oldTokens);
    }
}