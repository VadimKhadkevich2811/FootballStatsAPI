using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommand : IRequest<Response<bool>>
{
    public int PlayerId { get; set; }
    public string Name { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public int Age { get; set; }
    public PositionGroup Position { get; set; }
}