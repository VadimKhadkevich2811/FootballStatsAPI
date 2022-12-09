using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetAllCoaches;

public class GetAllCoachesQuery : IRequest<Response<CoachesListWithCountDto>>
{
    public CoachesQueryStringParams CoachesQueryStringParams { get; set; }
    public GetAllCoachesQuery(CoachesQueryStringParams coachesQueryStringParams)
    {
        CoachesQueryStringParams = coachesQueryStringParams;
    }
}