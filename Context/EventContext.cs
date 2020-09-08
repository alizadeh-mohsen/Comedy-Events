using ComedyEvents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Comedy_Events.Context
{
    public class EventContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Event> Events { get; set; }
        public DbSet<Comedian> Comedians { get; set; }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Venue> Venues { get; set; }


        public EventContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ComedyEvents"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    EventDate = DateTime.Now.AddDays(10),
                    EventName = "Funny Comedy Night",
                    VenueId = 1
                }
                );


            modelBuilder.Entity<Venue>().HasData(

                new Venue
                {
                    VenueId = 1,
                    City = "Wilkes Barre",
                    Seating = 125,
                    ServesAlcohol = false,
                    State = "PA",
                    Street = "123 Main Street",
                    VenueName = "Mohegan Sun",
                    ZipCode = "18702"
                });

            modelBuilder.Entity<Gig>().HasData(

                new Gig
                {
                    ComedianId = 1,
                    EventId = 1,
                    GigHeadline = "Pavols Funny Show",
                    GigId = 1,
                    GigLengthInMinutes = 60
                },
                new Gig
                {
                    ComedianId = 2,
                    EventId = 1,
                    GigHeadline = "Lifetime of Fun",
                    GigId = 2,
                    GigLengthInMinutes = 45
                }

                );
            modelBuilder.Entity<Comedian>().HasData(

                new Comedian
                {
                    FirstName = "Mohsen",
                    ComedianId = 1,
                    LastName = "Alizadeh",
                    ContactPhone = "123-456-789"
                },
                new Comedian
                {
                    FirstName = "Robbie",
                    ComedianId = 2,
                    LastName = "Williams",
                    ContactPhone = "111-222-333"
                }
                );
        }
    }
}
