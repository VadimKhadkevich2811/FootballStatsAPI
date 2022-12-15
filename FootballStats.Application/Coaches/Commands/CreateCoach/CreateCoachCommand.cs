using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.Application.Coaches.Commands.CreateCoach;

public class CreateCoachCommand : IRequest<Response<CoachReadDto>>
{
    public string Name { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public int Age { get; set; }
    public string Nationality { get; set; } = default!;
    public PositionGroup Position { get; set; }
}