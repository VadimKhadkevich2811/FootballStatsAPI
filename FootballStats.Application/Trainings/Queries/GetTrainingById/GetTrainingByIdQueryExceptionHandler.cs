using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Trainings.Queries.GetTrainingById;

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
        state.SetHandled(new Response<TrainingReadDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}