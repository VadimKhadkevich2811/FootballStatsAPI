using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Coaches.Commands.DeleteCoach;

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
        state.SetHandled(new Response<bool>(false, false, message, exception.Message));
        _logger.LogError(message);

    }
}