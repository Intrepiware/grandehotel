using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            Bookings = new HashSet<Bookings>();
        }

        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public decimal NightlyRate { get; set; }

        public virtual ICollection<Bookings> Bookings { get; set; }
    }
}
