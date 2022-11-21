using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;

public class CreateCoachCommand : IRequest<CoachReadDTO>
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
}