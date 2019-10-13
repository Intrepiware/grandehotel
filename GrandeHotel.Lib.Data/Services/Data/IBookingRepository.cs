using GrandeHotel.Lib.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Services.Data
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Guid CreateBooking(Guid roomId, DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
