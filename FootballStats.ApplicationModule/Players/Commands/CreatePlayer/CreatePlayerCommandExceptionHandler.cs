using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Players.Commands.CreatePlayer;

public class CreatePlayerCommandExceptionHandler : RequestExceptionHandler<CreatePlayerCommand, Response<PlayerReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public CreatePlayerCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(CreatePlayerCommand request, Exception exception, RequestExceptionHandlerState<Response<PlayerReadDto>> state)
    {
        string message = "Exception during creating player";
        state.SetHandled(new Response<PlayerReadDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}