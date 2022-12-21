using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Login.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Login.Commands;

public class LoginCommandExceptionHandler : RequestExceptionHandler<LoginCommand, Response<LoginDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public LoginCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(LoginCommand request, Exception exception, RequestExceptionHandlerState<Response<LoginDto>> state)
    {
        string message = "Exception during login operation";
        state.SetHandled(new Response<LoginDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}