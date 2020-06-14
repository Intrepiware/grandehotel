using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Data;
using GrandeHotel.Lib.Data.Services.Data.Impl;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Services.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GrandeHotelCustomContext _context;

        public UnitOfWork(GrandeHotelCustomContext context)
        {
            _context = context;
            Rooms = new RoomsRepository(context);
            Bookings = new BookingsRepository(context);
            Users = new UserRepository(context);
        }

        public IRoomRepository Rooms { get; }

        public IBookingRepository Bookings { get; }

        public IUserRepository Users { get; }

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
