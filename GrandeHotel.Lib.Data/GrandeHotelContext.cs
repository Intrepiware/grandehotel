using GrandeHotel.Lib.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GrandeHotel.Lib.Data
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

        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<ScriptsRun> ScriptsRun { get; set; }
        public virtual DbSet<ScriptsRunErrors> ScriptsRunErrors { get; set; }
        public virtual DbSet<Version> Version { get; set; }

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

            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("pk_bookings")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("bookings", "reservations");

                entity.Property(e => e.BookingId)
                    .HasColumnName("booking_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_bookings_room_id_rooms_room_id");
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("rooms", "reservations");

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

            modelBuilder.Entity<ScriptsRun>(entity =>
            {
                entity.ToTable("ScriptsRun", "RoundhousE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnteredBy)
                    .HasColumnName("entered_by")
                    .HasMaxLength(50);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("entry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OneTimeScript).HasColumnName("one_time_script");

                entity.Property(e => e.ScriptName)
                    .HasColumnName("script_name")
                    .HasMaxLength(255);

                entity.Property(e => e.TextHash)
                    .HasColumnName("text_hash")
                    .HasMaxLength(512);

                entity.Property(e => e.TextOfScript)
                    .HasColumnName("text_of_script")
                    .HasColumnType("text");

                entity.Property(e => e.VersionId).HasColumnName("version_id");
            });

            modelBuilder.Entity<ScriptsRunErrors>(entity =>
            {
                entity.ToTable("ScriptsRunErrors", "RoundhousE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnteredBy)
                    .HasColumnName("entered_by")
                    .HasMaxLength(50);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("entry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ErroneousPartOfScript)
                    .HasColumnName("erroneous_part_of_script")
                    .HasColumnType("ntext");

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("error_message")
                    .HasColumnType("ntext");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.RepositoryPath)
                    .HasColumnName("repository_path")
                    .HasMaxLength(255);

                entity.Property(e => e.ScriptName)
                    .HasColumnName("script_name")
                    .HasMaxLength(255);

                entity.Property(e => e.TextOfScript)
                    .HasColumnName("text_of_script")
                    .HasColumnType("ntext");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.ToTable("Version", "RoundhousE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnteredBy)
                    .HasColumnName("entered_by")
                    .HasMaxLength(50);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("entry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.RepositoryPath)
                    .HasColumnName("repository_path")
                    .HasMaxLength(255);

                entity.Property(e => e.Version1)
                    .HasColumnName("version")
                    .HasMaxLength(50);
            });
        }
    }
}
