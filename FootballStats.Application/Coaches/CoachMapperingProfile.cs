using AutoMapper;
using FootballStats.Application.Coaches.Commands.CreateCoach;
using FootballStats.Application.Coaches.Commands.UpdateCoach;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Domain.Entities;

namespace FootballStats.Application.Common.Mappings;

public class CoachMappingProfile : Profile
{
    public CoachMappingProfile()
    {
        CreateMap<Coach, CoachReadDto>();
        CreateMap<CreateCoachCommand, Coach>();
        CreateMap<UpdateCoachCommand, Coach>();
        CreateMap<Coach, UpdateCoachCommand>();
    }
}