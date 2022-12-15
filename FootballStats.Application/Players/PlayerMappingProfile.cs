using AutoMapper;
using FootballStats.Application.Players.Commands.CreatePlayer;
using FootballStats.Application.Players.Commands.UpdatePlayer;
using FootballStats.Application.Players.Dtos;
using FootballStats.Domain.Entities;

namespace FootballStats.Application.Players;

public class PlayerMappingProfile : Profile
{
    public PlayerMappingProfile()
    {
        CreateMap<Player, PlayerReadDto>();
        CreateMap<CreatePlayerCommand, Player>();
        CreateMap<UpdatePlayerCommand, Player>();
        CreateMap<Player, UpdatePlayerCommand>();
    }
}