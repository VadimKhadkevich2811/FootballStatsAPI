using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;

public class DeleteCoachCommand : IRequest<Response<bool>>
{
    public int CoachId { get; }

    public DeleteCoachCommand(int coachId)
    {
        CoachId = coachId;
    }
}