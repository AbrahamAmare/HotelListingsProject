using HotelListingsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelListingsApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Ethiopia",
                    SCO = "ETH"
                },

                 new Country
                {
                    Id = 2,
                    Name = "Caymen Iasland",
                    SCO = "CI   "
                },

                 new Country
                {
                    Id = 3,
                    Name = "Bahamas",
                    SCO = "BS"
                }
                
            );

            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    CountryId = 2,
                    Rating = 4.5
                },

                new Hotel
                {
                    Id = 2,
                    Name = "Harmony Hotel",
                    Address = "Addis Ababa",
                    CountryId = 1,
                    Rating = 4.2
                },

                new Hotel
                {
                    Id = 3,
                    Name = "Comfort Suites",
                    Address = "George Town",
                    CountryId = 3,
                    Rating = 4.7
                }
            );
        }

        public DbSet<Country> Countries { get; set;}
        public DbSet<Hotel> Hotel { get; set;}
    }
}