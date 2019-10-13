using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Services.Bookings.Impl
{
    public class BookingManager : IBookingManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid MakeReservation(Guid roomId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return _unitOfWork.Bookings.CreateBooking(roomId, startDate, endDate);
        }
    }
}
