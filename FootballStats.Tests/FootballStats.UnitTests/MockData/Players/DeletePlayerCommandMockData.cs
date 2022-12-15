using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;

namespace FootballStats.UnitTests.MockData.Players;

public class DeletePlayerCommandMockData
{
    public static DeletePlayerCommand GetTestPlayerCommandData()
    {
        return new DeletePlayerCommand(1);
    }

}