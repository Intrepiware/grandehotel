using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Tests.Integration.Services.Data
{
    [TestFixture, NonParallelizable]
    public class BookingRepositoryTests : IntegrationsTestBase
    {
        private readonly Guid _roomId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        [OneTimeSetUp]
        public void SetUp()
        {
            ExecuteScript("ClearTables.sql");
        }

        [Test]
        public void Add_Invoked_Throws()
        {
            Assert.ThrowsAsync<NotImplementedException>(async () =>
            await _unitOfWork.Bookings.Add(new Models.Booking()
            {
                Amount = 1,
                RoomId = _roomId,
                StartDate = new DateTimeOffset(2019, 1, 1, 12, 00, 00, TimeSpan.FromHours(-6)),
                EndDate = new DateTimeOffset(2019, 1, 2, 11, 59, 00, TimeSpan.FromHours(-6))
            }));
        }

        [Test]
        public void AddRange_Invoked_Throws()
        {
            Assert.ThrowsAsync<NotImplementedException>(async () =>
            await _unitOfWork.Bookings.AddRange(new[] {
                new Models.Booking
                {
                    Amount = 1,
                    RoomId = _roomId,
                    StartDate = new DateTimeOffset(2019, 1, 1, 12, 00, 00, TimeSpan.FromHours(-6)),
                    EndDate = new DateTimeOffset(2019, 1, 2, 11, 59, 00, TimeSpan.FromHours(-6))
                },
                new Models.Booking {
                    Amount = 1,
                    RoomId = _roomId,
                    StartDate = new DateTimeOffset(2019, 1, 2, 12, 00, 00, TimeSpan.FromHours(-6)),
                    EndDate = new DateTimeOffset(2019, 1, 3, 11, 59, 00, TimeSpan.FromHours(-6))
                }
            }));
        }
    }
}
