using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Models
{
    public class RoomAvailability
    {
        public Guid RoomId { get; set; }

        public string Name { get; set; }
        
        public DateTimeOffset CheckIn { get; set; }

        public DateTimeOffset CheckOut { get; set; }
    }
}
