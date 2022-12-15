using FootballStats.Application.Players.Commands.CreatePlayer;
using FootballStats.Domain.Enums;

namespace FootballStats.UnitTests.MockData.Players;

public class CreatePlayerCommandMockData
{
    public static CreatePlayerCommand GetEmptyCreatePlayerCommandData()
    {
        return new CreatePlayerCommand();
    }

    public static CreatePlayerCommand? GetNoCreatePlayerCommandData()
    {
        return null;
    }

    public static CreatePlayerCommand GetTestPlayerCommandData()
    {
        return new CreatePlayerCommand()
        {
            Name = "Phil",
            Lastname = "Test",
            Age = 30,
            Position = PositionGroup.Forward
        };
    }

}