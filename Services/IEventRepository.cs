using ComedyEvents.Models;
using System;
using System.Threading.Tasks;

namespace Comedy_Events.Services
{
    public interface IEventRepository
    {
        #region CRUD
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> Save();

        #endregion

        #region Events

        Task<Event[]> GetEvents(bool includeGigs = false);
        Task<Event> GetEvent(int EventId, bool includeGigs = false);
        Task<Event[]> GetEventsByDate(DateTime date, bool includeGigs = false);

        #endregion

        #region Gigs

        Task<Gig> GetGig(int gigId, bool includeComedians = false);
        Task<Gig[]> GetGigsByEvent(int EventId, bool includeComeidans = false);
        Task<Gig[]> GetGigsByVenue(int VenueId, bool includeComeidans = false);


        #endregion

        #region Comedians

        Task<Comedian[]> GetComedians();
        Task<Comedian[]> GetComediansByEvent(int EventId);
        Task<Comedian> GetComedian(int comedianId);


        #endregion



    }
}
