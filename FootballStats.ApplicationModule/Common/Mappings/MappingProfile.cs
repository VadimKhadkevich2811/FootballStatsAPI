using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using FootballStats.ApplicationModule.Common.Dtos;
using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;
using FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;
using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginDto>();
        CreateMap<User, SignUpDto>();
        CreateMap<Player, PlayerReadDto>();
        CreateMap<CreatePlayerCommand, Player>();
        CreateMap<UpdatePlayerCommand, Player>();
        CreateMap<Player, UpdatePlayerCommand>();

        CreateMap<Training, TrainingReadDto>();

        CreateMap<CreateTrainingCommand, Training>();
        CreateMap<UpdateTrainingCommand, Training>();
        CreateMap<Training, UpdateTrainingCommand>();

        CreateMap<Coach, CoachReadDto>();
        CreateMap<CreateCoachCommand, Coach>();
        CreateMap<UpdateCoachCommand, Coach>();
        CreateMap<Coach, UpdateCoachCommand>();
    }
}