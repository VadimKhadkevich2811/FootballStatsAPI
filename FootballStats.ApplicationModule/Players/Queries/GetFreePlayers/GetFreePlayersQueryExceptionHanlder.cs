using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;

public class GetFreePlayersQueryExceptionHandler : RequestExceptionHandler<GetFreePlayersQuery, Response<PlayersListWithCountDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetFreePlayersQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetFreePlayersQuery request, Exception exception, RequestExceptionHandlerState<Response<PlayersListWithCountDto>> state)
    {
        string message = "Exception during getting free players";
        state.SetHandled(new Response<PlayersListWithCountDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);
    }
}