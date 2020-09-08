using ComedyEvents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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


    }
}
