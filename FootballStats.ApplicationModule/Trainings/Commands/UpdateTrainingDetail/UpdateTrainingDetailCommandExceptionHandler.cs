using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Trainings.Commands.UpdateTrainingDetail;

public class UpdateTrainingDetailCommandExceptionHandler : RequestExceptionHandler<UpdateTrainingDetailCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public UpdateTrainingDetailCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(UpdateTrainingDetailCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during updating training details";
        state.SetHandled(new Response<bool>(false, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}