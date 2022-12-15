using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;

public class GetTrainingByIdQueryExceptionHandler : RequestExceptionHandler<GetTrainingByIdQuery, Response<TrainingReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetTrainingByIdQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetTrainingByIdQuery request, Exception exception, RequestExceptionHandlerState<Response<TrainingReadDto>> state)
    {
        string message = "Exception during getting training by Id";
        state.SetHandled(new Response<TrainingReadDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}