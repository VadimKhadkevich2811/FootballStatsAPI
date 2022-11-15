using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;

public class DeleteCoachCommand : IRequest<bool>
{
    public int CoachId { get; }

    public DeleteCoachCommand(int coachId)
    {
        CoachId = coachId;
    }
}