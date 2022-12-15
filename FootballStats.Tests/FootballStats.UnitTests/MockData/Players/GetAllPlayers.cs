using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.UnitTests.MockData.Players;

public class GetPlayersMockData
{
    public static IEnumerable<Player> GetAllPlayers()
    {
        return new List<Player>()
        {
            new() { Id = 1, Name = "Tom", Lastname = "Test", Age = 20, Position = PositionGroup.Midfielder },
            new() { Id = 2, Name = "Jack", Lastname = "Test2", Age = 40, Position = PositionGroup.Goalkeeper },
            new() { Id = 3, Name = "Mike", Lastname = "Test3", Age = 30, Position = PositionGroup.Forward },
        };
    }

}