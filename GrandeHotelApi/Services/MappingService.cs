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
