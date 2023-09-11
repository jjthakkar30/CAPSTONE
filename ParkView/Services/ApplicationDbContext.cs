using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkView.Models;

namespace ParkView.Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Hotel>().HasKey(h => h.HotelId);

            //builder.Entity<Hotel>().ToTable<Hotel>("Hotels");
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 1, Location = "Bangalore", ImgUrl = "/Images/bangalore_1.png", Desc = "Hotel", Rating = 4.6}
            );
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 2, Location = "Chennai", ImgUrl = "/Images/chennai_1.png", Desc = "Hotel", Rating = 4.6}
            );
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 3, Location = "Mumbai", ImgUrl = "/Images/mumbai_1.png", Desc = "Hotel", Rating = 4.6}
            );
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 4, Location = "Pune", ImgUrl = "/Images/pune_1.png", Desc = "Hotel", Rating = 4.6}
            );
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 5, Location = "Goa", ImgUrl = "/Images/goa_1.png", Desc = "Hotel", Rating = 4.6}
            );
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 6, Location = "Udaipur", ImgUrl = "/Images/udaipur_1.png", Desc = "Hotel", Rating = 4.6}
            );
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 7, Location = "Hyderbad", ImgUrl = "/Images/hyderabad_1.png", Desc = "Hotel", Rating = 4.6}
            );
            builder.Entity<Hotel>().HasData(
              new Hotel() { HotelId = 8, Location = "Delhi", ImgUrl = "/Images/delhi_1.png", Desc = "Hotel", Rating = 4.6}
            );
        }
    }
}