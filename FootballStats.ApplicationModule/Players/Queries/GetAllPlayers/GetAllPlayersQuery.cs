using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.QueryParams;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;

public class GetAllPlayersQuery : IRequest<PlayersListWithCountDTO>
{
    public PlayersQueryStringParams PlayersQueryStringParams { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public GetAllPlayersQuery(PlayersQueryStringParams playersQueryStringParams)
    {
        PlayersQueryStringParams = playersQueryStringParams;
    }
}