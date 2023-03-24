using Aydinturk_agency.Models;
using Aydinturk_agency.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aydinturk_agency.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Flight>()
            .HasOne(e => e.From)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict); // <--

            modelBuilder.Entity<Flight>()
                .HasOne(e => e.To)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // <--

        }

    }




}
