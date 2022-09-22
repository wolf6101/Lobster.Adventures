using AutoMapper;

using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Application.MappingProfiles
{
    public class UserJourneyMappingProfile : Profile
    {
        public UserJourneyMappingProfile()
        {
            CreateMap<UserJourney, UserJourneyDto>().ReverseMap();
        }
    }
}