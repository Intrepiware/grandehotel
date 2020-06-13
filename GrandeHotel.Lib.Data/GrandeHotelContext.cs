using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class GrandeHotelContext : DbContext
    {
        public GrandeHotelContext()
        {
        }

        public GrandeHotelContext(DbContextOptions<GrandeHotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=GrandeHotel;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("pk_booking")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("booking", "reservations");

                entity.Property(e => e.BookingId)
                    .HasColumnName("booking_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TimestampUtc)
                    .HasColumnName("timestamp_utc")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysutcdatetime())");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_booking_room_id_rooms_room_id");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("room", "reservations");

                entity.Property(e => e.RoomId)
                    .HasColumnName("room_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NightlyRate)
                    .HasColumnName("nightly_rate")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "reservations");

                entity.HasIndex(e => e.Email)
                    .HasName("uq_user_email")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
