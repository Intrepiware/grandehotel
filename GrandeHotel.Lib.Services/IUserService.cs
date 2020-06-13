using GrandeHotel.Models;

namespace GrandeHotel.Lib.Services
{
    public interface IUserService
    {
        int Create(UserCreateModel userModel);
    }
}
