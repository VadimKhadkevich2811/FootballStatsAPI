using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using FootballStats.Domain.Enums;

namespace FootballStats.UnitTests.MockData.Players;

public class UpdatePlayerCommandMockData
{
    public static UpdatePlayerCommand GetEmptyUpdatePlayerCommandData()
    {
        return new UpdatePlayerCommand();
    }

    public static UpdatePlayerCommand GetTestUpdatePlayerCommandData()
    {
        return new UpdatePlayerCommand()
        {
            PlayerId = 1,
            Name = "Phil",
            Lastname = "Test",
            Age = 30,
            Position = PositionGroup.Forward
        };
    }
}