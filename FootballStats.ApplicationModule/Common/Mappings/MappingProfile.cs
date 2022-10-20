using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.Domain.Entities;
using AutoMapper;

namespace FootballStats.ApplicationModule.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginDTO>();
        CreateMap<User, SignUpDTO>();  
    }
}