using AutoMapper;
using FootballStats.Application.Trainings.Commands.CreateTraining;
using FootballStats.Application.Trainings.Commands.UpdateTraining;
using FootballStats.Application.Trainings.Dtos;
using FootballStats.Domain.Entities;

namespace FootballStats.Application.Common.Mappings;

public class TrainingMappingProfile : Profile
{
    public TrainingMappingProfile()
    {
        CreateMap<Training, TrainingReadDto>();
        CreateMap<CreateTrainingCommand, Training>();
        CreateMap<UpdateTrainingCommand, Training>();
        CreateMap<Training, UpdateTrainingCommand>();
    }
}