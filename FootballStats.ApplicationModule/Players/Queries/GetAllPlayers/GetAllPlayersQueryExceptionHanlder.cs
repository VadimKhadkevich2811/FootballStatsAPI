using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Players.Queries.GetAllPlayers;

public class GetAllPlayersQueryExceptionHandler : RequestExceptionHandler<GetAllPlayersQuery, Response<PlayersListWithCountDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetAllPlayersQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetAllPlayersQuery request, Exception exception, RequestExceptionHandlerState<Response<PlayersListWithCountDto>> state)
    {
        string message = "Exception during getting players";
        state.SetHandled(new Response<PlayersListWithCountDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}