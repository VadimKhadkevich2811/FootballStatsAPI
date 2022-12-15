using FootballStats.Application.Common.Wrappers;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.Application.Coaches.Commands.UpdateCoach;

public class UpdateCoachCommand : IRequest<Response<bool>>
{
    public int CoachId { get; set; }
    public string Name { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public int Age { get; set; }
    public string Nationality { get; set; } = default!;
    public PositionGroup Position { get; set; }
}