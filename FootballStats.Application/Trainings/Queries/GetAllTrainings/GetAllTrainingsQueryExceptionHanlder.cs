using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Trainings.Queries.GetAllTrainings;

public class GetAllTrainingsQueryExceptionHandler : RequestExceptionHandler<GetAllTrainingsQuery, Response<TrainingsListWithCountDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetAllTrainingsQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetAllTrainingsQuery request, Exception exception, RequestExceptionHandlerState<Response<TrainingsListWithCountDto>> state)
    {
        string message = "Exception during getting trainings";
        state.SetHandled(new Response<TrainingsListWithCountDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}