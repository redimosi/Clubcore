using AutoMapper;
using Clubcore.Api.Models;
using Clubcore.Domain.AggregatesModel;
using Clubcore.Domain.Models;

namespace Clubcore.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Club, ClubDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.LastName))
                .ForMember(dest => dest.MobileNr, opt => opt.MapFrom(src => src.Name.MobileNr));

            CreateMap<PersonDto, Person>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new PersonName
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    MobileNr = src.MobileNr
                }));
        }
    }
}
