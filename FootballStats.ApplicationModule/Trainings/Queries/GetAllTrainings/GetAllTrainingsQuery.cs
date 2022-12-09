using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainings;

public class GetAllTrainingsQuery : IRequest<Response<TrainingsListWithCountDto>>
{
    public TrainingsQueryStringParams TrainingsQueryStringParams { get; set; }
    public GetAllTrainingsQuery(TrainingsQueryStringParams trainingsQueryStringParams)
    {
        TrainingsQueryStringParams = trainingsQueryStringParams;
    }
}