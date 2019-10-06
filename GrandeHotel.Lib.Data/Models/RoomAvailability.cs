using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrandeHotel.Lib.Data.Models
{
    public class RoomAvailability
    {
        [Key]
        public Guid RoomId { get; set; }

        public string Name { get; set; }
        
        [Key]
        public DateTimeOffset CheckIn { get; set; }

        public DateTimeOffset CheckOut { get; set; }
    }
}
