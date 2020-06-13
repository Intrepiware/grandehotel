using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Services.Security
{
    public interface IPasswordHashService
    {
        string Hash(string password);
        bool Validate(string message, string hash);
    }
}
