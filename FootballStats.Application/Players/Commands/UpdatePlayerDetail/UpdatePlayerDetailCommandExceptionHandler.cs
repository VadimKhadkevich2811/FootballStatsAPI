using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Players.Commands.UpdatePlayerDetail;

public class UpdatePlayerDetailCommandExceptionHandler : RequestExceptionHandler<UpdatePlayerDetailCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public UpdatePlayerDetailCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(UpdatePlayerDetailCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during updating player details";
        state.SetHandled(new Response<bool>(false, false, message, exception.Message));
        _logger.LogError(message);

    }
}