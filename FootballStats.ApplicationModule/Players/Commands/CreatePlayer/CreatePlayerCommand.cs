using FootballStats.ApplicationModule.Common.DTOs.Players;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Commands.CreatePlayer;

public class CreatePlayerCommand : IRequest<PlayerReadDTO>
{
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public int Age { get; set; }
    public string? Club { get; set; }
}