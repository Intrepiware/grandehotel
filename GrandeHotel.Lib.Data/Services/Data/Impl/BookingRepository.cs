using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Repository.Impl;
using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Services.Data.Impl
{
    public class BookingsRepository : Repository<Booking>, IBookingRepository
    {
        public BookingsRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }

    }
}
