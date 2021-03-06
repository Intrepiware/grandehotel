﻿using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class Booking
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Room Room { get; set; }
    }
}
