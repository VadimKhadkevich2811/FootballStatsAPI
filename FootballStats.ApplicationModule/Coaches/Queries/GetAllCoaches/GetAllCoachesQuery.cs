using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.QueryParams;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetAllCoaches;

public class GetAllCoachesQuery : IRequest<CoachesListWithCountDTO>
{
    public CoachesQueryStringParams CoachesQueryStringParams { get; set; }
    public GetAllCoachesQuery(CoachesQueryStringParams coachesQueryStringParams)
    {
        CoachesQueryStringParams = coachesQueryStringParams;
    }
}