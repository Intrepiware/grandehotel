using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Models.Exceptions;
using GrandeHotel.Lib.Data.Services.Bookings.Impl;
using GrandeHotel.Lib.Data.Services.Impl;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GrandeHotel.Lib.Data.Tests.Integration.Services.Bookings
{
    [TestFixture]
    public class BookingManagerStressTests: IntegrationsTestBase
    {
        Guid _roomId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        [OneTimeSetUp]
        public async Task SetUp()
        {
            ExecuteScript("ClearTables.sql");
            ExecuteScript("Services\\Data\\BookingRepositoryStressTests.sql");
            await CreateBookings(100);
        }

        [Test]
        // Makes 100 near-simultaneous conflicting reservations; tests that no conflicting reservations are persisted to the database
        public void MakeReservation_100Conflicts_ZeroOrOneSave()
        {
            int rowCount = ExecuteScalar<int>("select count(*) from reservations.booking");
            Assert.LessOrEqual(rowCount, 1);
        }

        // Create 100 near-simultaneous conflicting reservations
        // I'm doing a lot of fuzzing here, which makes the test less deterministic, but I think a better test
        // for this circumstance
        private async Task CreateBookings(int rounds)
        {
            Random random = new Random();
            ActionBlock<Tuple<DateTimeOffset, DateTimeOffset>> actionBlock = new ActionBlock<Tuple<DateTimeOffset, DateTimeOffset>>(async times =>
            {
                // Fuzz: add a random delay of when reservation attempt actually hits database
                await Task.Delay(random.Next(0, 2000)); 
                using (UnitOfWork uow = GetUnitOfWork())
                {
                    BookingManager manager = new BookingManager(uow);
                    try
                    {
                        manager.MakeReservation(_roomId, times.Item1, times.Item2);
                    }
                    catch (BookingException) { }
                }

            },
            new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 15 });

            foreach(int num in Enumerable.Range(0, rounds))
            {
                // Fuzz: use different start/end dates; any two of these times, if they were actually saved, would create conflicting reservations
                switch(num % 3)
                {
                    case 0:
                        actionBlock.Post(Tuple.Create(new DateTimeOffset(2019, 1, 1, 0, 0, 0, default), new DateTimeOffset(2019, 1, 6, 0, 0, 0, default)));
                        break;
                    case 1:
                        actionBlock.Post(Tuple.Create(new DateTimeOffset(2019, 1, 5, 0, 0, 0, default), new DateTimeOffset(2019, 1, 6, 0, 0, 0, default)));
                        break;
                    case 2:
                        actionBlock.Post(Tuple.Create(new DateTimeOffset(2019, 1, 5, 0, 0, 0, default), new DateTimeOffset(2019, 1, 10, 0, 0, 0, default)));
                        break;
                }
            }

            actionBlock.Complete();
            await actionBlock.Completion;
        }

        private UnitOfWork GetUnitOfWork()
        {
            DbContextOptionsBuilder<GrandeHotelContext> optionsBuilder = new DbContextOptionsBuilder<GrandeHotelContext>();
            var options = optionsBuilder.UseSqlServer(TestConnectionString);
            return new UnitOfWork(new GrandeHotelCustomContext(options.Options));
        }
    }
}
