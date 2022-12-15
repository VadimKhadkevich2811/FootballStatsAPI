using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Players.Queries.GetAllPlayers;

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
        state.SetHandled(new Response<PlayersListWithCountDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}