using GrandeHotel.Models;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Services.Security
{
    public interface IAuthenticationService
    {
        Task<UserTokenModel> Authenticate(string username, string password);
        string CreateToken(int userId);
    }
}
