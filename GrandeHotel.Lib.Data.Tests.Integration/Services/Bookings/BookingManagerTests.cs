using GrandeHotel.Lib.Data.Models.Exceptions;
using GrandeHotel.Lib.Data.Services.Bookings.Impl;
using NUnit.Framework;
using System;

namespace GrandeHotel.Lib.Data.Tests.Integration.Services.Bookings
{
    [TestFixture, NonParallelizable]
    public class BookingManagerTests : IntegrationsTestBase
    {
        private BookingManager _bookingManager;
        Guid _roomId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        Guid _bookingId = Guid.Parse("00000000-0000-0000-0000-000000000010");
        static TimeSpan _cst = TimeSpan.FromHours(-6), _cdt = TimeSpan.FromHours(-5);

        private static object[] ConflictingBookingTimes = new[]
        {
            // Overlaps first day of existing reservation
            new object[] { new DateTimeOffset(2018, 12, 30, 12, 00, 00, _cst), new DateTimeOffset(2019, 1, 2, 11, 59, 0, _cst) },
            // Overlaps last day of existing reservation
            new object[] { new DateTimeOffset(2019, 1, 4, 12, 00, 00, _cst), new DateTimeOffset(2019, 1, 6, 11, 59, 0 , _cst) },
            // Conflicts with day in middle of existing reservation
            new object[] { new DateTimeOffset(2019, 1, 2, 12, 00, 00, _cst), new DateTimeOffset(2019, 1, 3, 11, 59, 0 , _cst) },
            // Supercedes existing reservation
            new object[] { new DateTimeOffset(2018, 12, 28, 12, 00, 00, _cst), new DateTimeOffset(2019, 1, 10, 11, 59, 0 , _cst) }
        };

        private static readonly DateTimeOffset[][] NonConflictingBookingTimes = new[]
        {
            // Reservation ends right before existing reservation
            new DateTimeOffset[] { new DateTimeOffset(2018, 12, 30, 12, 00, 00, _cst), new DateTimeOffset(2019, 1, 1, 11, 59, 0 , _cst) },
            // Reservation begins right after existing reservation
            new DateTimeOffset[] { new DateTimeOffset(2019, 1, 5, 12, 00, 00, _cst), new DateTimeOffset(2019, 1, 7, 11, 59, 0 , _cst) },
            // No proximity to existing reservation
            new DateTimeOffset[] { new DateTimeOffset(2019, 3, 1, 12, 00, 00, _cdt), new DateTimeOffset(2019, 3, 3, 11, 59, 0, _cdt) },

        };

        [OneTimeSetUp]
        public void SetUp()
        {
            ExecuteScript("ClearTables.sql");
            ExecuteScript("Services\\Bookings\\BookingManagerTests.sql");
            _bookingManager = new BookingManager(_unitOfWork);
            RunNonConflictingReservations();
        }

        [Test, Parallelizable]
        public void MakeReservation_NoConflict_MakesReservation()
        {
            var expected = new[] // Matches the "NonConflictingBookingTimes" set above
            {
                new { room_id = _roomId, start_date = new DateTimeOffset(2018, 12, 30, 12, 00, 00, _cst), end_date = new DateTimeOffset(2019, 1, 1, 11, 59, 0, _cst), amount = 1},
                new { room_id = _roomId, start_date = new DateTimeOffset(2019, 1, 5, 12, 00, 00, _cst), end_date = new DateTimeOffset(2019, 1, 7, 11, 59, 0, _cst), amount = 1},
                new { room_id = _roomId, start_date = new DateTimeOffset(2019, 3, 1, 12, 00, 00, _cdt), end_date = new DateTimeOffset(2019, 3, 3, 11, 59, 0, _cdt), amount = 1}

            };

            SqlResultMatches(expected, $"select room_id, start_date, end_date, amount from reservations.booking where booking_id <> '{_bookingId}' order by start_date");
        }

        [Test, Parallelizable]
        [TestCaseSource("ConflictingBookingTimes")]
        public void MakeReservation_HasConflict_Throws(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            BookingException exception = Assert.Throws<BookingException>(() => _bookingManager.MakeReservation(_roomId, startDate, endDate));

            StringAssert.Contains("conflict", exception.Message);
        }

        [Test, Parallelizable]
        public void MakeReservation_EndDateBeforeStartDate_Throws()
        {
            BookingException exception = Assert.Throws<BookingException>(() => _bookingManager.MakeReservation(_roomId, new DateTimeOffset(2000, 2, 1, 0, 0, 0, _cst), new DateTimeOffset(2000, 1, 1, 0, 0, 0, _cst)));
            StringAssert.Contains("End Date", exception.Message);
        }


        private void RunNonConflictingReservations()
        {
            foreach(DateTimeOffset[] nonConflict in NonConflictingBookingTimes)
            {
                _bookingManager.MakeReservation(_roomId, nonConflict[0], nonConflict[1]);
            }
        }
    }
}
