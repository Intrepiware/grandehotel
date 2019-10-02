﻿using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Repository.Impl;
using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Services.Data.Impl
{
    public class BookingsRepository : Repository<Bookings>, IBookingsRepository
    {
        public BookingsRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }

        public IEnumerable<Bookings> GetAvailableRooms()
        {
            throw new NotImplementedException();
        }
    }
}
