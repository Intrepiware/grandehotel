using GrandeHotel.Lib.Data.Services.Data;
using System;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository Rooms { get; }
        IBookingRepository Bookings { get; }
        Task<int> Complete();
    }
}
