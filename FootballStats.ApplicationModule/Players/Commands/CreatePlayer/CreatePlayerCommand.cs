using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Commands.CreatePlayer;

public class CreatePlayerCommand : IRequest<PlayerReadDTO>
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public PositionGroup Position { get; set; }
}