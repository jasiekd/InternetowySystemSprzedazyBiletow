using System.Security.Cryptography;
using System.Text;
using Google.Apis.Auth;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class AccountService
    {
        private readonly TokenService _tokenService;
        private readonly DataContext _context;
        public AccountService(TokenService tokenService, DataContext context)
        {
            _tokenService = tokenService;
            _context = context;
    }

        public TokenInfoDto LoginUser(UserLoginRequestDto loginData)
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            else
            {
                var accountEntity = _context.Accounts.Where(x=> x.Login == loginData.UserName).FirstOrDefault();
                if (accountEntity!=null) {
                    SHA256 sha256 = SHA256Managed.Create();
                    byte[] bytes = Encoding.UTF8.GetBytes(loginData.Password);
                    byte[] hash = sha256.ComputeHash(bytes);
                    string password = Convert.ToBase64String(hash);
                    if (accountEntity.Password == password) {
                        var result = new TokenInfoDto();
                        result.AccessToken = _tokenService.GenerateBearerToken(accountEntity.Id.ToString());
                        result.RefreshToken = _tokenService.GenerateRefreshToken(accountEntity.Id.ToString());

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<GoogleJsonWebSignature.Payload> GetGooglePayload(string idToken)
        {
            //Will throw a InvalidJwtException if the passed value is not valid JWT
            GoogleJsonWebSignature.Payload validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken);

            return validPayload;
        }

        private bool EmailAlreadyExist(string email)
        {
            return (_context.Accounts?.Any(e => e.Email == email)).GetValueOrDefault();
        }
        public TokenInfoDto LoginUserWithGoogle(GoogleLoginRequest loginData)
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            else
            {
                var payload = GetGooglePayload(loginData.Credentials);

                if(EmailAlreadyExist(payload.Result.Email))
                {
                    var accountEntity = _context.Accounts.Where(x => x.Email == payload.Result.Email).FirstOrDefault();
                    var result = new TokenInfoDto();
                    result.AccessToken = _tokenService.GenerateBearerToken(accountEntity.Id.ToString());
                    result.RefreshToken = _tokenService.GenerateRefreshToken(accountEntity.Id.ToString());

                    return result;
                }
                else
                {
                    var newAccountEntity = new AccountEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = payload.Result.GivenName,
                        Surname = payload.Result.FamilyName,
                        Email = payload.Result.Email,
                        DateOfBirth = new DateTime(2008, 3, 1, 7, 0, 0),
                        IsDeleted = false,
                        RoleID = 2,
                        GoogleSubject = payload.Result.Subject
                    };

                    _context.Accounts.Add(newAccountEntity);
                    _context.SaveChangesAsync();
                    var result = new TokenInfoDto();
                    result.AccessToken = _tokenService.GenerateBearerToken(newAccountEntity.Id.ToString());
                    result.RefreshToken = _tokenService.GenerateRefreshToken(newAccountEntity.Id.ToString());

                    return result;
                    
                }
                return null;
            }
        }
    }
}
