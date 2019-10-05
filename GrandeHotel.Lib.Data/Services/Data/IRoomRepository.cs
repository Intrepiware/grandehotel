using GrandeHotel.Lib.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Services.Data
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IList<RoomAvailability>> GetRoomAvailabilities(int days, int checkInOffsetMinutes, Func<RoomAvailability, bool> filter = null);
    }
}
