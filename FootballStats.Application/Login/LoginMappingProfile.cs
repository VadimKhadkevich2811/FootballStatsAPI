using AutoMapper;
using FootballStats.Application.Login.Dtos;
using FootballStats.Domain.Entities;

namespace FootballStats.Application.Common.Mappings;

public class LoginMappingProfile : Profile
{
    public LoginMappingProfile()
    {
        CreateMap<User, LoginDto>();
    }
}