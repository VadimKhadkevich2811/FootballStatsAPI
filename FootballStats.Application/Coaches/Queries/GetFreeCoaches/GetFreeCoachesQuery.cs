using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Coaches.Queries.GetFreeCoaches;

public class GetFreeCoachesQuery : IRequest<Response<CoachesListWithCountDto>>
{
    public DateTime Date { get; }
    public GetFreeCoachesQuery(DateTime date)
    {
        Date = date;
    }
}