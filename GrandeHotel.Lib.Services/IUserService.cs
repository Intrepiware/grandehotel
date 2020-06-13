using GrandeHotel.Models;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Services
{
    public interface IUserService
    {
        Task<int> Create(UserCreateModel userModel);
    }
}
