using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Data;
using GrandeHotel.Lib.Data.Services.Data.Impl;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Services.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GrandeHotelContext _context;

        public UnitOfWork(GrandeHotelContext context)
        {
            _context = context;
            Rooms = new RoomsRepository(context);
            Bookings = new BookingsRepository(context);
        }

        public IRoomRepository Rooms { get; private set; }

        public IBookingRepository Bookings { get; private set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
