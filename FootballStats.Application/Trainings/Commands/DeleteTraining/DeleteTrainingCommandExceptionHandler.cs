using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Trainings.Commands.DeleteTraining;

public class DeleteTrainingCommandExceptionHandler : RequestExceptionHandler<DeleteTrainingCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public DeleteTrainingCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(DeleteTrainingCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during deleting training";
        state.SetHandled(new Response<bool>(false, false, message, exception.Message));
        _logger.LogError(message);

    }
}