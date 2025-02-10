using DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    internal class AppContext : DbContext
    {

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookingCalendar> BookingCalendars { get; set; }
        public DbSet<BookingSlot> BookingSlots { get; set; }
        public DbSet<Promo> Promos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AboutMe>()
                .HasMany(a => a.Photos)
                .WithOne(p => p.AboutMe)
                .HasForeignKey(p => p.AboutMeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Service>()
                .HasMany(s => s.Images)
                .WithOne(i => i.Service)
                .HasForeignKey(i => i.ServiceId);

            modelBuilder.Entity<BookingCalendar>()
                        .HasMany(bc => bc.Slots)
                        .WithOne(bs => bs.BookingCalendar)
                        .HasForeignKey(bs => bs.BookingCalendarId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingSlot>()
                        .Property(bs => bs.Status)
                        .HasConversion<int>();

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<int>();


            modelBuilder.Entity<Promo>(entity =>
            {
                entity.HasIndex(p => p.Title).IsUnique();
                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.DiscountDescription)
                    .HasMaxLength(500);
                entity.Property(p => p.OldPrice)
                    .HasMaxLength(50);
                entity.Property(p => p.NewPrice)
                    .HasMaxLength(50);

                entity.Property(p => p.Benefits)
                    .HasConversion(
                        v => string.Join(",", v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
            });

        }
    }
}
