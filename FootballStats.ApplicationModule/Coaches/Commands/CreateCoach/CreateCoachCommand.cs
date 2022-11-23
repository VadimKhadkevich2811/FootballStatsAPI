using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;

public class CreateCoachCommand : IRequest<CoachReadDTO>
{
    public string Name { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public int Age { get; set; }
    public PositionGroup Position { get; set; }
}