using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Coaches.Commands.UpdateCoachDetail;

public class UpdateCoachDetailCommandExceptionHandler : RequestExceptionHandler<UpdateCoachDetailCommand, Response<bool>, Exception>
{
    private readonly ILoggerManager _logger;

    public UpdateCoachDetailCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(UpdateCoachDetailCommand request, Exception exception, RequestExceptionHandlerState<Response<bool>> state)
    {
        string message = "Exception during updating coach details";
        state.SetHandled(new Response<bool>(false, false, message, exception.Message));
        _logger.LogError(message);

    }
}