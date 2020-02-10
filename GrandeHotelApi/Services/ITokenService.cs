using System.Threading.Tasks;

namespace GrandeHotelApi.Services
{
    public interface ITokenService
    {
        Task<string> GetToken();
    }
}
