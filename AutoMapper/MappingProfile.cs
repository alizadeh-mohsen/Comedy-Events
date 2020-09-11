using AutoMapper;
using ComedyEvents.Dto;
using ComedyEvents.Models;

namespace Comedy_Events.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comedian, ComedianDto>();
            CreateMap<ComedianDto, Comedian>();
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<VenueDto, Venue>();
            CreateMap<Venue, VenueDto>();
            CreateMap<GigDto, Gig>();
            CreateMap<Gig, GigDto>();

        }
    }
}
