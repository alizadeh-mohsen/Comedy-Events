using AutoMapper;
using Comedy_Events.Services;
using ComedyEvents.Dto;
using ComedyEvents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;

namespace Comedy_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public EventsController(IEventRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<EventDto[]>> Get(bool includeGigs = false)
        {
            var events = await _repository.GetEvents(includeGigs);

            if (!events.Any())
                return NotFound();

            var eventDtos = _mapper.Map<EventDto[]>(events);
            return Ok(eventDtos);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventDto>> Get(int eventId, bool includeGigs = false)
        {
            var singleEvent = await _repository.GetEvent(eventId, includeGigs);

            if (singleEvent == null)
                return NotFound();

            var eventDto = _mapper.Map<EventDto>(singleEvent);
            return Ok(eventDto);
        }

        [HttpGet("search")]
        public async Task<ActionResult<EventDto[]>> SearchByDate(DateTime date, bool includeGigs = true)
        {
            var events = await _repository.GetEventsByDate(date, includeGigs);
            if (!events.Any())
                return NotFound();

            var eventDtos = _mapper.Map<EventDto[]>(events);
            return Ok(eventDtos);
        }

        [HttpPost]
        public async Task<ActionResult<EventDto>> Post(EventDto dto)
        {
            var eventToSave = _mapper.Map<Event>(dto);
            _repository.Add(eventToSave);

            if (await _repository.Save())
            {
                var link = _linkGenerator.GetPathByAction("Get", "Events", new { eventToSave.EventId });
                dto.EventId = eventToSave.EventId;
                return Created(link, dto);
            }
            return BadRequest();
        }

    }
}
