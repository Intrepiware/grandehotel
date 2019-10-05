using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Repository.Impl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Services.Data.Impl
{
    public class RoomsRepository : Repository<Room>, IRoomRepository
    {
        public RoomsRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }

        public async Task<IList<RoomAvailability>> GetRoomAvailabilities(int days, int checkInOffsetMinutes, Func<RoomAvailability, bool> filter = null)
        {
            List<RoomAvailability> output = await Context.Set<RoomAvailability>().FromSql("reservations.usp_get_availability {0}, {1}", days, checkInOffsetMinutes).ToListAsync();

            if (filter != null) output = output.Where(filter).ToList();

            return output;
        }
    }
}
