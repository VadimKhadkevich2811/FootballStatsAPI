using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Coaches.Commands.DeleteCoach;

public class DeleteCoachCommand : IRequest<Response<bool>>
{
    public int CoachId { get; }

    public DeleteCoachCommand(int coachId)
    {
        CoachId = coachId;
    }
}