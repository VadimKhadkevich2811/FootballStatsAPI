using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Trainings.Commands.UpdateTraining;

public class UpdateTrainingCommandExceptionHandler : RequestExceptionHandler<UpdateTrainingCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public UpdateTrainingCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(UpdateTrainingCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during updating training";
        state.SetHandled(new Response<bool>(false, false, message, exception.Message));
        _logger.LogError(message);

    }
}