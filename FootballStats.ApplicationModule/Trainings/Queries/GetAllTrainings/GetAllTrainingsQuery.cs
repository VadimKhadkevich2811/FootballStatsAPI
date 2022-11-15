using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Filters;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainingsQuery;

public class GetAllTrainingsQuery : IRequest<TrainingsListWithCountDTO>
{
    public PaginationFilter PaginationFilter { get; set; }
    public GetAllTrainingsQuery(PaginationFilter paginationFilter)
    {
        PaginationFilter = paginationFilter;
    }
}