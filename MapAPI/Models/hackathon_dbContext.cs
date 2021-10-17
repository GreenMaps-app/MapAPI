using Microsoft.EntityFrameworkCore;
using MapAPI.Helpers;

/**
 * Automatically created by scanning through the database
 */
#nullable disable

namespace MapAPI.Models
{
    public partial class hackathon_dbContext : DbContext
    {
        public hackathon_dbContext()
        {
        }

        public hackathon_dbContext(DbContextOptions<hackathon_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MapLocationDatum> MapLocationData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connStr = DBHelpers.GetSqlConnectionString("hackathonDBConn");
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MapLocationDatum>(entity =>
            {
                entity.ToTable("map_location_data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ip_address");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Message)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Resolved)
                    .HasColumnName("resolved");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
