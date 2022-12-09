using FootballStats.ApplicationModule.Common.Dtos;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Login.Commands;

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
        state.SetHandled(new Response<LoginDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}