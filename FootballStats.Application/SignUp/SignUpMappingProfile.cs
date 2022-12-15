using AutoMapper;
using FootballStats.Application.SignUp.Dtos;
using FootballStats.Domain.Entities;

namespace FootballStats.Application.Common.Mappings;

public class SignUpMappingProfile : Profile
{
    public SignUpMappingProfile()
    {
        CreateMap<User, SignUpDto>();
    }
}