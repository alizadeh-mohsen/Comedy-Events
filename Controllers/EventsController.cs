using AutoMapper;
using Comedy_Events.Services;
using ComedyEvents.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Comedy_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;


        public EventsController(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EventDto[]>> Get(bool includeGigs = false)
        {
            var events = await _repository.GetEvents(includeGigs);
            var eventDtos = _mapper.Map<EventDto[]>(events);
            return Ok(eventDtos);
        }

        //[HttpGet]
        //public async Task<ActionResult<Event>> Get(int eventId, bool includeGigs = false)
        //{
        //    var result = await _repository.GetEvent(eventId, includeGigs);
        //    return Ok(result);
        //}



    }
}
