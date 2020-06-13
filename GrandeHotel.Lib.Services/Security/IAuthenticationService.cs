using GrandeHotel.Models;

namespace GrandeHotel.Lib.Services.Security
{
    public interface IAuthenticationService
    {
        UserTokenModel Authenticate(string username, string password);
        string CreateToken(int userId);
    }
}
