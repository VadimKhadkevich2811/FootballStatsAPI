using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.Domain.Entities;
using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

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
    }
}