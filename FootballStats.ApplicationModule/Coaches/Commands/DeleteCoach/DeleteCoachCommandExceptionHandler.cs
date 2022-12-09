using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;

public class DeleteCoachCommandExceptionHandler : RequestExceptionHandler<DeleteCoachCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public DeleteCoachCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(DeleteCoachCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during deleting coach";
        state.SetHandled(new Response<bool>(false, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}