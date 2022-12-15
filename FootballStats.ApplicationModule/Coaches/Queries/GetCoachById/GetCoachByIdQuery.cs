using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetCoachById;

public class GetCoachByIdQuery : IRequest<Response<CoachReadDto>>
{
    public int CoachId { get; }

    public GetCoachByIdQuery(int coachId)
    {
        CoachId = coachId;
    }
}