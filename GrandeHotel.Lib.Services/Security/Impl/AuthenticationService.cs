using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Services.Security.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService _passwordHashService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUnitOfWork unitOfWork,
            IPasswordHashService passwordHashService,
            IOptions<AppSettings> appSettings)
        {
            _passwordHashService = passwordHashService;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task<UserTokenModel> Authenticate(string username, string password)
        {
            if (username != null)
            {
                var user = (await _unitOfWork.Users.Find(u => u.Email.ToLower() == username.ToLower())).SingleOrDefault();
                if (_passwordHashService.Validate(password, user.Password))
                {
                    return new UserTokenModel
                    {
                        Token = CreateToken(user.UserId),
                        UserId = user.UserId
                    };
                }
            }
            return null;
        }

        public string CreateToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = _appSettings.Jwt;
            var key = Convert.FromBase64String(jwt.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwt.ExpirationInMinutes),
                Audience = jwt.Audience,
                Issuer = jwt.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}