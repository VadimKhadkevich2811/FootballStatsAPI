using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;

public class CreateCoachCommandExceptionHandler : RequestExceptionHandler<CreateCoachCommand, Response<CoachReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public CreateCoachCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(CreateCoachCommand request, Exception exception, RequestExceptionHandlerState<Response<CoachReadDto>> state)
    {
        string message = "Exception during creating coach";
        state.SetHandled(new Response<CoachReadDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}