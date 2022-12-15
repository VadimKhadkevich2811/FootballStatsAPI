using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayers;

namespace FootballStats.UnitTests.MockData.Players;

public class GetAllPlayersQueryMockData
{
    public static GetAllPlayersQuery GetAllPlayersQueryData()
    {
        return new GetAllPlayersQuery(new PlayersQueryStringParams());
    }

}