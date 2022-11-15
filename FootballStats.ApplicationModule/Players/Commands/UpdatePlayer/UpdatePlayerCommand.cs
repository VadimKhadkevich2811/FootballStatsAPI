using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommand : IRequest<bool>
{
    public int PlayerId { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public int Age { get; set; }
    public PositionGroup Position { get; set; }
}