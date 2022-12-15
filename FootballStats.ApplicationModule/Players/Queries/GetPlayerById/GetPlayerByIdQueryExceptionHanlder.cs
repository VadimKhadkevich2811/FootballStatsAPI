using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Players.Queries.GetPlayerById;

public class GetPlayerByIdQueryExceptionHandler : RequestExceptionHandler<GetPlayerByIdQuery, Response<PlayerReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetPlayerByIdQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetPlayerByIdQuery request, Exception exception, RequestExceptionHandlerState<Response<PlayerReadDto>> state)
    {
        string message = "Exception during getting player by Id";
        state.SetHandled(new Response<PlayerReadDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}