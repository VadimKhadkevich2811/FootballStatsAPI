using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Players.Queries.GetFreePlayers;

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
        state.SetHandled(new Response<PlayersListWithCountDto>(null, false, message, exception.Message));
        _logger.LogError(message);
    }
}