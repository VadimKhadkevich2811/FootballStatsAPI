using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetCoachById;

public class GetCoachByIdQueryExceptionHandler : RequestExceptionHandler<GetCoachByIdQuery, Response<CoachReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetCoachByIdQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetCoachByIdQuery request, Exception exception, RequestExceptionHandlerState<Response<CoachReadDto>> state)
    {
        string message = "Exception during getting coach by Id";
        state.SetHandled(new Response<CoachReadDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}