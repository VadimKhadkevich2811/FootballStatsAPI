using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommandExceptionHandler : RequestExceptionHandler<UpdatePlayerCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public UpdatePlayerCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(UpdatePlayerCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during updating player";
        state.SetHandled(new Response<bool>(false, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}