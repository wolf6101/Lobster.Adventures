using AutoMapper;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Application.MappingProfiles
{
    public class AdventureMappingProfile : Profile
    {
        public AdventureMappingProfile()
        {
            CreateMap<Adventure, AdventureDto>()
                .ForMember(a => a.Nodes, opt => opt.MapFrom(a => a.Nodes))
                .ReverseMap();
        }
    }
}