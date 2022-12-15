using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;

public class CreateCoachCommand : IRequest<Response<CoachReadDto>>
{
    public string Name { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public int Age { get; set; }
    public PositionGroup Position { get; set; }
}