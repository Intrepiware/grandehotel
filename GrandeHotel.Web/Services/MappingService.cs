using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Web.Models.Api;

namespace GrandeHotel.Web.Services
{
    public static class MappingService
    {
        public static Room ToRoom(RoomPostModel data)
        {
            return new Room()
            {
                Name = data.Name,
                NightlyRate = data.NightlyRate
            };
        }

        public static RoomGetModel ToRoomGetModel(Room room)
        {
            return new RoomGetModel()
            {
                Name = room.Name,
                NightlyRate = room.NightlyRate,
                RoomId = room.RoomId
            };
        }
    }
}
