using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Players.Queries.GetAllPlayers;

namespace FootballStats.UnitTests.MockData.Players;

public class GetAllPlayersQueryMockData
{
    public static GetAllPlayersQuery GetAllPlayersQueryData()
    {
        return new GetAllPlayersQuery(new PlayersQueryStringParams());
    }

}