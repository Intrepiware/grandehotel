using GrandeHotel.Lib.Data.Models;
using GrandeHotelApi.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotelApi.Services
{
    public static class MappingService
    {
        public static Rooms ToRooms(RoomPostModel data)
        {
            return new Rooms()
            {
                Name = data.Name,
                NightlyRate = data.NightlyRate
            };
        }

        public static RoomGetModel ToRoomGetModel(Rooms room)
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
