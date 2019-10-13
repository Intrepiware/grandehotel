using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Repository.Impl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Data.Services.Data.Impl
{
    public class BookingsRepository : Repository<Booking>, IBookingRepository
    {
        public BookingsRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }

        new public async Task Add(Booking entity)
        {
            await Task.CompletedTask;
            throw new NotImplementedException($"Use {nameof(CreateBooking)} instead");
        }

        new public async Task AddRange(IEnumerable<Booking> entities)
        {
            await Task.CompletedTask;
            throw new NotImplementedException($"Use {nameof(CreateBooking)} instead");
        }

        public Guid CreateBooking(Guid roomId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            using (DbConnection connection = Context.Database.GetDbConnection())
            using(DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "reservations.usp_create_booking";
                cmd.CreateParameterWithValue("room_id", roomId);
                cmd.CreateParameterWithValue("start_date", startDate);
                cmd.CreateParameterWithValue("end_date", endDate);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                return (Guid)cmd.ExecuteScalar();
            }
        }

    }
}
