using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetAllPlayers;

public class GetAllPlayersQuery : IRequest<Response<PlayersListWithCountDto>>
{
    public PlayersQueryStringParams PlayersQueryStringParams { get; set; }
    public GetAllPlayersQuery(PlayersQueryStringParams playersQueryStringParams)
    {
        PlayersQueryStringParams = playersQueryStringParams;
    }
}