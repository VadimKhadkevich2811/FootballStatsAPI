using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Trainings.Commands.DeleteTraining;

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
        state.SetHandled(new Response<bool>(false, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}