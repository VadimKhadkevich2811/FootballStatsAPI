using FootballStats.Application.Trainings.Dtos;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Trainings.Queries.GetAllTrainings;

public class GetAllTrainingsQuery : IRequest<Response<TrainingsListWithCountDto>>
{
    public TrainingsQueryStringParams TrainingsQueryStringParams { get; set; }
    public GetAllTrainingsQuery(TrainingsQueryStringParams trainingsQueryStringParams)
    {
        TrainingsQueryStringParams = trainingsQueryStringParams;
    }
}