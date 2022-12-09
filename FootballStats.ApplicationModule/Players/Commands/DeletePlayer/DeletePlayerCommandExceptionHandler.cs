using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Players.Commands.DeletePlayer;

public class DeletePlayerCommandExceptionHandler : RequestExceptionHandler<DeletePlayerCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public DeletePlayerCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(DeletePlayerCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during deleting player";
        state.SetHandled(new Response<bool>(false, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}