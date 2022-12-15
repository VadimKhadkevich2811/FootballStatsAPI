using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Coaches.Queries.GetAllCoaches;

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
        state.SetHandled(new Response<CoachesListWithCountDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}