using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Coaches.Queries.GetAllCoaches;

public class GetAllCoachesQuery : IRequest<Response<CoachesListWithCountDto>>
{
    public CoachesQueryStringParams CoachesQueryStringParams { get; set; }
    public GetAllCoachesQuery(CoachesQueryStringParams coachesQueryStringParams)
    {
        CoachesQueryStringParams = coachesQueryStringParams;
    }
}