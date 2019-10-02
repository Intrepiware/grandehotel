using GrandeHotel.Lib.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Services.Data
{
    public interface IBookingsRepository : IRepository<Bookings>
    {
        IEnumerable<Bookings> GetAvailableRooms();
    }
}
