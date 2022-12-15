using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Coaches.Queries.GetFreeCoaches;

public class GetFreeCoachesQueryExceptionHandler : RequestExceptionHandler<GetFreeCoachesQuery, Response<CoachesListWithCountDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetFreeCoachesQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetFreeCoachesQuery request, Exception exception, RequestExceptionHandlerState<Response<CoachesListWithCountDto>> state)
    {
        string message = "Error during getting free coaches";
        state.SetHandled(new Response<CoachesListWithCountDto>(null, false, message, exception.Message));
        _logger.LogError(message);
    }
}