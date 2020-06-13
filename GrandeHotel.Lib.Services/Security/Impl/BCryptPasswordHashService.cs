using System;
using System.Collections.Generic;
using System.Text;
using static BCrypt.Net.BCrypt;

namespace GrandeHotel.Lib.Services.Security.Impl
{
    public class BCryptPasswordHashService : IPasswordHashService
    {
        public string Hash(string password) => HashPassword(password);
        public bool Validate(string message, string hash) => Verify(message, hash)
    }
}
