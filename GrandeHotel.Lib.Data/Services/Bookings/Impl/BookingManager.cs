using GrandeHotel.Lib.Data.Models.Exceptions;
using System;
using System.Data.Common;

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
            if (endDate < startDate) throw new BookingException("The booking's End Date is before the Start Date");

            try
            {
                return _unitOfWork.Bookings.CreateBooking(roomId, startDate, endDate);
            }
            catch(DbException dbEx) when (dbEx.Message.Contains("conflicts with another booking") || dbEx.Message.Contains("deadlock"))
            {
                throw new BookingException("Booking conflicts with another booking");
            }
        }
    }
}
