using FootballStats.ApplicationModule.Common.Dtos;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.SignUp.Commands;

public class SignUpCommandExceptionHandler : RequestExceptionHandler<SignUpCommand, Response<SignUpDto?>, Exception>
{
    private readonly ILoggerManager _logger;

    public SignUpCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(SignUpCommand request, Exception exception, RequestExceptionHandlerState<Response<SignUpDto?>> state)
    {
        string message = "Exception during sign up operation";
        state.SetHandled(new Response<SignUpDto?>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}