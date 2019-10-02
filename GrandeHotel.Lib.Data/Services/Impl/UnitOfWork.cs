using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GrandeHotel.Lib.Data.Services.Data;
using GrandeHotel.Lib.Data.Services.Data.Impl;

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

        public IRoomsRepository Rooms { get; private set; }

        public IBookingsRepository Bookings { get; private set; }

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
