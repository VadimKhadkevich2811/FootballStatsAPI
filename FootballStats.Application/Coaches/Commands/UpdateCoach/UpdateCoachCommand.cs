using FootballStats.Application.Common.Wrappers;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.Application.Coaches.Commands.UpdateCoach;

public class UpdateCoachCommand : IRequest<Response<bool>>
{
    public int CoachId { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public int Age { get; set; }
    public PositionGroup Position { get; set; }
}