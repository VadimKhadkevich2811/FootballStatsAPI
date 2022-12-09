using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;

public class GetFreeCoachesQuery : IRequest<Response<CoachesListWithCountDto>>
{
    public DateTime Date { get; }
    public GetFreeCoachesQuery(DateTime date)
    {
        Date = date;
    }
}