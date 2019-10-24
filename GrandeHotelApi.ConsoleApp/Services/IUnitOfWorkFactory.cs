using GrandeHotel.Lib.Data.Services;

namespace GrandeHotelApi.ConsoleApp.Services
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Generate();
    }
}
