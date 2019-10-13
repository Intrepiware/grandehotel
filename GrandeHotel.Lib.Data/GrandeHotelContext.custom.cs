using Microsoft.EntityFrameworkCore;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class GrandeHotelCustomContext : GrandeHotelContext
    {
        public GrandeHotelCustomContext()
        {
        }

        public GrandeHotelCustomContext(DbContextOptions<GrandeHotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RoomAvailability> RoomAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoomAvailability>(entity =>
            {
                entity.HasKey(ra => new { ra.RoomId, ra.CheckIn });

                entity.Property(e => e.CheckIn)
                    .HasColumnName("check_in")
                    .HasColumnType("datetimeoffset");

                entity.Property(e => e.CheckOut)
                    .HasColumnName("check_out")
                    .HasColumnType("datetimeoffset");

                entity.Property(e => e.Name)
                    .HasColumnName("name");

                entity.Property(e => e.RoomId)
                    .HasColumnName("room_id")
                    .HasColumnType("uniqueidentifier");
            });
        }
    }
}
