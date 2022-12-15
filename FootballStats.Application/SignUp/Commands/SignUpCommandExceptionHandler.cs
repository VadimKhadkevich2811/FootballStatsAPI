using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.SignUp.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.SignUp.Commands;

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
        state.SetHandled(new Response<SignUpDto?>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}