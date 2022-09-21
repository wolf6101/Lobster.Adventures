using AutoMapper;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Application.MappingProfiles
{
    public class AdventureNodeMappingProfile : Profile
    {
        public AdventureNodeMappingProfile()
        {
            CreateMap<AdventureNode, AdventureNodeDto>().ReverseMap();
        }
    }
}