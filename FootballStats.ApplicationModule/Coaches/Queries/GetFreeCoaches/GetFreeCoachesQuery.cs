using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;

public class GetFreeCoachesQuery : IRequest<CoachesListWithCountDTO>
{
    public DateTime Date { get; }
    public GetFreeCoachesQuery(DateTime date)
    {
        Date = date;
    }
}