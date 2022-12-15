using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Coaches.Commands.UpdateCoach;

public class UpdateCoachCommandExceptionHandler : RequestExceptionHandler<UpdateCoachCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public UpdateCoachCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(UpdateCoachCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during updating coach";
        state.SetHandled(new Response<bool>(false, false, message, exception.Message));
        _logger.LogError(message);

    }
}