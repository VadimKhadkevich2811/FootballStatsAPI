using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.Filters;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetAllCoachesQuery;

public class GetAllCoachesQuery : IRequest<CoachesListWithCountDTO>
{
    public PaginationFilter PaginationFilter { get; set; }
    public GetAllCoachesQuery(PaginationFilter paginationFilter)
    {
        PaginationFilter = paginationFilter;
    }
}