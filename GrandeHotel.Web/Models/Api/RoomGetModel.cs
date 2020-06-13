using System;

namespace GrandeHotel.Web.Models.Api
{
    public class RoomGetModel
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public decimal NightlyRate { get; set; }

    }
}
