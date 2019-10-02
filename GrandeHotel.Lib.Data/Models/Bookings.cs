using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class Bookings
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal Amount { get; set; }

        public virtual Rooms Room { get; set; }
    }
}
