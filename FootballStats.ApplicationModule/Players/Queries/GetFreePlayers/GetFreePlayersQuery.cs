using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.QueryParams;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;

public class GetFreePlayersQuery : IRequest<PlayersListWithCountDTO>
{
    public PlayersQueryStringParams PlayersQueryStringParams { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public GetFreePlayersQuery(PlayersQueryStringParams playersQueryStringParams)
    {
        PlayersQueryStringParams = playersQueryStringParams;
    }
}