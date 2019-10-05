using GrandeHotel.Lib.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GrandeHotel.Lib.Data
{
    public partial class GrandeHotelContext : DbContext
    {
        public virtual DbSet<RoomAvailability> RoomAvailabilities { get; set; }
    }
}
