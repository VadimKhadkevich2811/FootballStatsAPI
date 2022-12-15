using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;

public class GetFreeCoachesQueryExceptionHandler : RequestExceptionHandler<GetFreeCoachesQuery, Response<CoachesListWithCountDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetFreeCoachesQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetFreeCoachesQuery request, Exception exception, RequestExceptionHandlerState<Response<CoachesListWithCountDto>> state)
    {
        string message = "Exception during getting free coaches";
        state.SetHandled(new Response<CoachesListWithCountDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);
    }
}