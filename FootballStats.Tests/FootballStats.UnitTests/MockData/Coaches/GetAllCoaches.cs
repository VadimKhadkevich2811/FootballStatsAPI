using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.UnitTests.MockData.Coaches;

public class GetCoachesMockData
{
    public static IEnumerable<Coach> GetAllCoaches()
    {
        return new List<Coach>()
        {
            new() { Id = 1, Name = "Name1", Lastname = "Lastname1", Position = PositionGroup.Goalkeeper, Age = 20 },
            new() { Id = 2, Name = "Name2", Lastname = "Lastname2", Position = PositionGroup.Deffender, Age = 30  },
            new() { Id = 3, Name = "Name3", Lastname = "Lastname3", Position = PositionGroup.Midfielder, Age = 40  },
        };
    }

}