using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

namespace FootballStats.UnitTests.MockData;

public class UpdatePlayerCommandMockData
{
    public static UpdatePlayerCommand GetEmptyUpdatePlayerCommandData()
    {
        return new UpdatePlayerCommand();
    }

}