using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;

namespace FootballStats.UnitTests.MockData.Players;

public class CreatePlayerCommandMockData
{
    public static CreatePlayerCommand GetEmptyCreatePlayerCommandData()
    {
        return new CreatePlayerCommand();
    }

    public static CreatePlayerCommand GetNoCreatePlayerCommandData()
    {
        return null;
    }

}