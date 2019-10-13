using GrandeHotel.Lib.Data.Services.Bookings.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using GrandeHotel.Lib.Data.Models.Exceptions;
using GrandeHotel.Lib.Data.Services.Impl;
using GrandeHotel.Lib.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GrandeHotel.Lib.Data.Tests.Integration.Services.Bookings
{
    [TestFixture]
    public class BookingManagerStressTests: IntegrationsTestBase
    {
        Guid _roomId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        TimeSpan _cst = TimeSpan.FromHours(-6);

        [OneTimeSetUp]
        public void SetUp()
        {
            ExecuteScript("ClearTables.sql");
            CreateBookings(100);
        }

        [Test]
        // Makes 100 simultaneous duplicate reservations; tests that no conflicting reservations are persisted to the database
        public void MakeReservation_100Conflicts_ZeroOrOneSave()
        {
            int rowCount = ExecuteScalar<int>("select count(*) from reservations.booking");
            Assert.LessOrEqual(rowCount, 1);
        }

        private void CreateBookings(int rounds)
        {
            Parallel.ForEach(Enumerable.Range(0, rounds),
                new ParallelOptions { MaxDegreeOfParallelism = 15 },
                _ =>
                {
                    using (UnitOfWork uow = GetUnitOfWork())
                    {
                        BookingManager manager = new BookingManager(uow);
                        try
                        {
                            manager.MakeReservation(_roomId, new DateTimeOffset(2019, 1, 1, 0, 0, 0, _cst), new DateTimeOffset(2019, 1, 2, 0, 0, 0, _cst));
                        }
                        catch (BookingException ex) { }
                    }
                }
            );
        }

        private UnitOfWork GetUnitOfWork()
        {
            DbContextOptionsBuilder<GrandeHotelContext> optionsBuilder = new DbContextOptionsBuilder<GrandeHotelContext>();
            var options = optionsBuilder.UseSqlServer(TestConnectionString);
            return new UnitOfWork(new GrandeHotelCustomContext(options.Options));
        }
    }
}
