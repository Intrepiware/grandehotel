using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Services.Security.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService _passwordHashService;
        private readonly IUnitOfWorkFactory _uowFactory;

        public AuthenticationService(IUnitOfWorkFactory unitOfWorkFactory,
            IPasswordHashService passwordHashService)
        {
            _passwordHashService = passwordHashService;
            _uowFactory = unitOfWorkFactory;
        }

        public async Task<UserTokenModel> Authenticate(string username, string password)
        {
            if (username != null)
            {
                using (var uow = _uowFactory.Generate())
                {
                    var user = (await uow.Users.Find(u => u.Email.ToLower() == username.ToLower())).SingleOrDefault();
                    if (_passwordHashService.Validate(password, user.Password))
                    {
                        return new UserTokenModel
                        {
                            Token = "tbd",
                            UserId = user.UserId
                        };
                    }
                }
            }
            return null;
        }

        public string CreateToken(int userId) => throw new NotImplementedException();
    }
}