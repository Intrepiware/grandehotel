using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
        }

        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public decimal NightlyRate { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
