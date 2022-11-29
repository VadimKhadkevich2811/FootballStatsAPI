using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetCoachById;

public class GetCoachByIdQuery : IRequest<CoachReadDTO>
{
    public int CoachId { get; }

    public GetCoachByIdQuery(int coachId)
    {
        CoachId = coachId;
    }
}