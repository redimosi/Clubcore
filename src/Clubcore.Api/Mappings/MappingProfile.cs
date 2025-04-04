using AutoMapper;
using Clubcore.Api.Models;
using Clubcore.Domain.AggregatesModel;

namespace Clubcore.Api.Mappings
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
