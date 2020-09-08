using Comedy_Events.Context;
using ComedyEvents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Comedy_Events.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext _eventContext;
        private readonly ILogger<EventRepository> _logger;
        EventRepository(EventContext context, ILogger<EventRepository> logger)
        {
            _eventContext = context;
            _logger = logger;
        }

        #region CRUD
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            _eventContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _eventContext.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _eventContext.Update(entity);
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _eventContext.SaveChangesAsync()) >= 0;
        }

        #endregion

        #region Events
        public async Task<Event> GetEvent(int eventId, bool includeGigs = false)
        {
            _logger.LogInformation($"Getting event id={eventId}");

            IQueryable<Event> query = _eventContext.Events.Include(v => v.Venue).Where(e => e.EventId == eventId);

            if (includeGigs)
            {
                query = query.Include(e => e.Gigs).ThenInclude(g => g.Comedian);
            }

            return await query.FirstOrDefaultAsync();

        }

        public async Task<Event[]> GetEvents(bool includeGigs = false)
        {
            _logger.LogInformation($"get all events");

            IQueryable<Event> events = _eventContext.Events.Include(e => e.Venue);


            if (includeGigs)
            {
                events = events.Include(e => e.Gigs).ThenInclude(g => g.Comedian);
            }

            return await events.ToArrayAsync();

        }

        public async Task<Event[]> GetEventsByDate(DateTime date, bool includeGigs = false)
        {
            _logger.LogInformation($"getting events if date {date}");

            IQueryable<Event> events = _eventContext.Events.Include(e => e.Venue).Where(e => e.EventDate.Date.Equals(date.Date));

            if (includeGigs)
                events.Include(e => e.Gigs).ThenInclude(g => g.Comedian);

            return await events.ToArrayAsync();
        }


        #endregion

        #region Gigs
        public async Task<Gig> GetGig(int gigId, bool includeComedians = false)
        {
            _logger.LogInformation($"Gettnig gig with id {gigId}");

            IQueryable<Gig> gig = _eventContext.Gigs.Include(g => g.Event).Where(g => g.GigId == gigId);

            if (includeComedians)
                gig = gig.Include(g => g.Comedian);

            return await gig.SingleOrDefaultAsync();


        }

        public async Task<Gig[]> GetGigsByEvent(int eventId, bool includeComeidans = false)
        {
            _logger.LogInformation($"Gettnig gigs of event id {eventId}");

            IQueryable<Gig> gigs = _eventContext.Gigs.Include(g => g.Event).Where(g => g.EventId == eventId);

            if (includeComeidans)
                gigs = gigs.Include(g => g.Comedian);

            return await gigs.OrderByDescending(g => g.GigHeadline).ToArrayAsync();
        }

        public async Task<Gig[]> GetGigsByVenue(int venueId, bool includeComeidans = false)
        {

            _logger.LogInformation($"Gettnig gigs of venue id {venueId}");

            IQueryable<Gig> gigs = _eventContext.Gigs.Include(g => g.Event.Venue).Where(g => g.Event.VenueId == venueId);

            if (includeComeidans)
                gigs = gigs.Include(g => g.Comedian);

            return await gigs.OrderByDescending(g => g.GigHeadline).ToArrayAsync();
        }

        #endregion

        #region Comedian
        public async Task<Comedian> GetComedian(int comedianId)
        {
            _logger.LogInformation($"Getting comedian with id {comedianId}");
            return await _eventContext.Comedians.FirstOrDefaultAsync(c => c.ComedianId == comedianId);
        }

        public async Task<Comedian[]> GetComedians()
        {
            _logger.LogInformation($"Getting all comedians");
            return await _eventContext.Comedians.ToArrayAsync();
        }

        public async Task<Comedian[]> GetComediansByEvent(int eventId)
        {
            _logger.LogInformation($"Getting all comedians");
            IQueryable<Gig> gigs = _eventContext.Gigs.Include(g => g.Comedian).Where(g => g.EventId == eventId);
            return await gigs.Select(c => c.Comedian).ToArrayAsync();
        }

        #endregion
    }
}
