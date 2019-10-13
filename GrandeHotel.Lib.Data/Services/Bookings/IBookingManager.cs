using System;

namespace GrandeHotel.Lib.Data.Services.Bookings
{
    public interface IBookingManager
    {
        Guid MakeReservation(Guid roomId, DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
