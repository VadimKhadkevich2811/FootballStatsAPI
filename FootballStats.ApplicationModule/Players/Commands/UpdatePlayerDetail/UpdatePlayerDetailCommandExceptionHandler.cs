using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayerDetail;

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
        state.SetHandled(new Response<bool>(false, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}