using GrandeHotel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GrandeHotel.Lib.Data.Services;

namespace GrandeHotel.Lib.Services.Security.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService _passwordHashService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork,
            IPasswordHashService passwordHashService)
        {
            _passwordHashService = passwordHashService;
            _unitOfWork = unitOfWork;
        }
        public UserTokenModel Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
        public string CreateToken(int userId) => throw new NotImplementedException();
    }
}
