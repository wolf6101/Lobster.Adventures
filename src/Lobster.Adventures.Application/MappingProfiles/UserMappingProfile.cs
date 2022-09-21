using AutoMapper;

using Lobster.Adventures.Application.Users.Dtos;
using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}