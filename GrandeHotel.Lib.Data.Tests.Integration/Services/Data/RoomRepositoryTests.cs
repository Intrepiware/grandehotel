using GrandeHotel.Lib.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Tests.Integration.Services.Data
{
    [TestFixture, NonParallelizable]
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

        [Test, Parallelizable]
        public void Delete_Invoked_DeletesRow()
        {
            SqlResultMatches(new dynamic[0], $"select 1 as [row] from reservations.room where room_id = '{_deletedId}'");
        }

        [Test, Parallelizable]
        public async Task Get_Invoked_GetsRow()
        {
            Room room = await _unitOfWork.Rooms.Get(_selectedId);
            Assert.AreEqual("room-will-be-selected", room.Name);
            Assert.AreEqual(1, room.NightlyRate);
        }

        [Test, Parallelizable]
        public async Task GetAll_Invoked_GetsAll()
        {
            List<Room> rooms = (await _unitOfWork.Rooms.GetAll()).OrderBy(room => room.RoomId).ToList();
            Assert.AreEqual(3, rooms.Count);

            Assert.AreEqual(_selectedId, rooms[0].RoomId);
            Assert.AreEqual("room-will-be-selected", rooms[0].Name);
            Assert.AreEqual(1, rooms[0].NightlyRate);

            Assert.AreEqual(_updatedId, rooms[1].RoomId);
            Assert.AreEqual(_insertId, rooms[2].RoomId);

        }

        [Test, Parallelizable]
        public void Update_Invoked_UpdatesRow()
        {
            SqlResultMatches(new[] { new { name = "updated-name", nightly_rate = 2 } }, $"select name, nightly_rate from reservations.room where room_id='{_updatedId}'");
        }

        [Test, Parallelizable]
        public void Add_Invoked_InsertsRow()
        {
            SqlResultMatches(new[] { new { name = "this-was-inserted", nightly_rate = 3 } }, $"select name, nightly_rate from reservations.room where room_id = '{_insertId}'");
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
