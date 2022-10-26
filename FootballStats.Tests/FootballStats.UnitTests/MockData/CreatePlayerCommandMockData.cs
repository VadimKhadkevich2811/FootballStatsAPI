using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;

namespace FootballStats.UnitTests.MockData;

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