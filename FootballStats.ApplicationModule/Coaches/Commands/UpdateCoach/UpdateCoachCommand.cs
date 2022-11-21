using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;

public class UpdateCoachCommand : IRequest<bool>
{
    public int CoachId { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public int Age { get; set; }
}