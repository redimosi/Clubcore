using AutoMapper;
using Clubcore.Entities;
using ClubcoreApi.Models;

namespace ClubcoreApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Club, ClubDto>();
            CreateMap<Group, GroupDto>();
            // Add other mappings as needed
        }
    }
}
