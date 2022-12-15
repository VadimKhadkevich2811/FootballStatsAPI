using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Commands.CreatePlayer;

public class CreatePlayerCommand : IRequest<Response<PlayerReadDto>>
{
    public string Name { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public int Age { get; set; }
    public PositionGroup Position { get; set; }
}