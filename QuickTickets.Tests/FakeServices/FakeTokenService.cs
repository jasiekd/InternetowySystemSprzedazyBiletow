using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;
using QuickTickets.Api.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuickTickets.Tests.FakeServices
{

    
    public class FakeTokenService : ITokenService
    {
        private readonly TokenOptions _tokenOptions;

        public FakeTokenService(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }
        public string GenerateBearerToken(string id, string role)
        {
            var expiry = DateTimeOffset.Now.AddMinutes(60); //ważny przez 15 minut
            var userClaims = GetClaimsForUser(id, role);
            return CreateToken(expiry, userClaims);
        }

        public string GenerateRefreshToken(string id, string role)
        {
            var expiry = DateTimeOffset.Now.AddDays(30); //ważny przez 30 dni
            var userClaims = GetClaimsForUser(id, role);
            return CreateToken(expiry, userClaims);
        }

        private string CreateToken(DateTimeOffset expiryDate, IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenOptions.SigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiryDate.DateTime,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private IEnumerable<Claim> GetClaimsForUser(string id, string role)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
            claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }
        public TokenInfoDto RefreshBearerToken(TokenInfoDto oldTokens)
        {
            throw new NotImplementedException();
        }
    }
}
