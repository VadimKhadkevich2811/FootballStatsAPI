using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Players.Commands.CreatePlayer;

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
        state.SetHandled(new Response<PlayerReadDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}