using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetAllCoaches;

public class GetAllCoachesQueryExceptionHandler : RequestExceptionHandler<GetAllCoachesQuery, Response<CoachesListWithCountDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetAllCoachesQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetAllCoachesQuery request, Exception exception, RequestExceptionHandlerState<Response<CoachesListWithCountDto>> state)
    {
        string message = "Exception during getting coaches";
        state.SetHandled(new Response<CoachesListWithCountDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}