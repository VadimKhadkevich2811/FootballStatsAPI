using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

namespace FootballStats.UnitTests.MockData.Players;

public class UpdatePlayerCommandMockData
{
    public static UpdatePlayerCommand GetEmptyUpdatePlayerCommandData()
    {
        return new UpdatePlayerCommand();
    }

}