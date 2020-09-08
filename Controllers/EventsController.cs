using Comedy_Events.Services;
using ComedyEvents.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Comedy_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public EventsController(IEventRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Event[]>> Get(bool includeGigs = false)
        {
            var result = await _repository.GetEvents(includeGigs);
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<ActionResult<Event>> Get(int eventId, bool includeGigs = false)
        //{
        //    var result = await _repository.GetEvent(eventId, includeGigs);
        //    return Ok(result);
        //}



    }
}
