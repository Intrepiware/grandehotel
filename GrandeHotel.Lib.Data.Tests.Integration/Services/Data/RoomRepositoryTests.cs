using GrandeHotel.Lib.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Tests.Integration.Services.Data
{
    [TestFixture]
    public class RoomRepositoryTests : IntegrationsTestBase
    {
        private Guid _selectedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
             _updatedId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            _deletedId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
            _insertId = Guid.Parse("00000000-0000-0000-0000-000000000004");

        [OneTimeSetUp]
        public async Task SetUp()
        {
            ExecuteScript("ClearTables.sql");
            ExecuteScript("Services\\Data\\RoomRepositoryTests.sql");
            await RunDbUpdates();
        }

        [Test]
        public void DummyTest()
        {
            Assert.Pass();
        }

        private async Task RunDbUpdates()
        {
            Room updated = await _unitOfWork.Rooms.Get(_updatedId);
            updated.Name = "updated-name";
            updated.NightlyRate = 2;

            Room delete = await _unitOfWork.Rooms.Get(_deletedId);
            _unitOfWork.Rooms.Remove(delete);

            Room insert = new Room
            {
                Name = "this-was-inserted",
                NightlyRate = 3,
                RoomId = _insertId
            };
            await _unitOfWork.Rooms.Add(insert);
            await _unitOfWork.Complete();
        }
    }
}
