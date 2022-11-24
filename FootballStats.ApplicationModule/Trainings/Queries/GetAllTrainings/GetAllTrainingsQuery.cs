using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Filters;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainingsQuery;

public class GetAllTrainingsQuery : IRequest<TrainingsListWithCountDTO>
{
    public TrainingsQueryStringParams TrainingsQueryStringParams { get; set; }
    public GetAllTrainingsQuery(TrainingsQueryStringParams trainingsQueryStringParams)
    {
        TrainingsQueryStringParams = trainingsQueryStringParams;
    }
}