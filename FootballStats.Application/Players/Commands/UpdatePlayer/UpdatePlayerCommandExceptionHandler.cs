using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Players.Commands.UpdatePlayer;

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
        state.SetHandled(new Response<bool>(false, false, message, exception.Message));
        _logger.LogError(message);

    }
}