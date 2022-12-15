using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainings;

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
        state.SetHandled(new Response<TrainingsListWithCountDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}