using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.Domain.Entities;
using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;
using FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;
using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;

namespace FootballStats.ApplicationModule.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginDTO>();
        CreateMap<User, SignUpDTO>();
        CreateMap<Player, PlayerReadDTO>();
        CreateMap<CreatePlayerCommand, Player>();
        CreateMap<UpdatePlayerCommand, Player>();
        CreateMap<Player, UpdatePlayerCommand>();
        CreateMap<Training, TrainingReadDTO>()
            .ForMember(dest => dest.CoachId,
                opt => opt.MapFrom(sou => sou.Coach.Id));

        CreateMap<CreateTrainingCommand, Training>();
        CreateMap<UpdateTrainingCommand, Training>();
        CreateMap<Training, UpdateTrainingCommand>();

        CreateMap<CreateCoachCommand, Coach>();
        CreateMap<UpdateCoachCommand, Coach>();
        CreateMap<Coach, UpdateCoachCommand>();
    }
}