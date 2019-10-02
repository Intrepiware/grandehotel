using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotelApi.Models.Api
{
    public class RoomGetModel
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public decimal NightlyRate { get; set; }

    }
}
