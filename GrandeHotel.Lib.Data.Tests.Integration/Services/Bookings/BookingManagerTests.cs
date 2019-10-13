using GrandeHotel.Lib.Data.Models.Exceptions;
using GrandeHotel.Lib.Data.Services.Bookings.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Tests.Integration.Services.Bookings
{
    [TestFixture, NonParallelizable]
    public class BookingManagerTests : IntegrationsTestBase
    {
        private BookingManager _bookingManager;
        Guid _roomId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        private static readonly object[] ConflictingBookingTimes = new[]
        {
            // Overlaps first day of existing reservation
            new object[] { new DateTimeOffset(2018, 12, 30, 12, 00, 00, TimeSpan.FromHours(-5)), new DateTimeOffset(2019, 1, 1, 11, 59, 0, TimeSpan.FromHours(-5)) },
            // Overlaps last day of existing reservation
            new object[] { new DateTimeOffset(2019, 1, 4, 12, 00, 00, TimeSpan.FromHours(-5)), new DateTimeOffset(2019, 1, 6, 11, 59, 0 , TimeSpan.FromHours(-5)) },
            // Conflicts with day in middle of existing reservation
            new object[] { new DateTimeOffset(2019, 1, 2, 12, 00, 00, TimeSpan.FromHours(-5)), new DateTimeOffset(2019, 1, 3, 11, 59, 0 , TimeSpan.FromHours(-5)) },
            // Supercedes existing reservation
            new object[] { new DateTimeOffset(2018, 12, 28, 12, 00, 00, TimeSpan.FromHours(-5)), new DateTimeOffset(2019, 1, 10, 11, 59, 0 , TimeSpan.FromHours(-5)) }
        };

        [OneTimeSetUp]
        public void SetUp()
        {
            ExecuteScript("ClearTables.sql");
            ExecuteScript("Services\\Bookings\\BookingManagerTests.sql");
            _bookingManager = new BookingManager(_unitOfWork);
        }

        [Test]
        public void MakeReservation_NoConflict_MakesReservation()
        {
            DateTimeOffset start_date = new DateTimeOffset(2019, 10, 12, 12, 00, 00, TimeSpan.FromHours(-5));
            DateTimeOffset end_date = new DateTimeOffset(2019, 10, 13, 11, 59, 0, TimeSpan.FromHours(-5));

            // Act
            Guid resrvId = _bookingManager.MakeReservation(_roomId, start_date, end_date);

            // Assert
            SqlResultMatches(new[] {
                new {
                    booking_id = resrvId,
                    room_id = _roomId,
                    start_date,
                    end_date,
                    amount = 1
                }}, $"select booking_id, room_id, start_date, end_date, amount from reservations.booking where booking_id='{resrvId}'");
        }

        [Test]
        [TestCaseSource("ConflictingBookingTimes")]
        public void MakeReservation_HasConflict_Throws(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            BookingException exception = Assert.Throws<BookingException>(() => _bookingManager.MakeReservation(_roomId, startDate, endDate));

            StringAssert.Contains("conflict", exception.Message);
        }

    }
}
