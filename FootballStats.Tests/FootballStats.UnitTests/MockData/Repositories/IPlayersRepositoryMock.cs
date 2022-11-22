using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using Moq;

namespace FootballStats.UnitTests.MockData.Repositories;

public class IPlayersRepositoryMock
{
    public static Mock<IPlayersRepository> GetMock()
    {
        var mock = new Mock<IPlayersRepository>();
        var players = new List<Player>()
        {
            new Player()
            {
                Id = 1,
                Name = "John",
                Lastname = "Test",
                Age = 20,
                Position = PositionGroup.Forward
            }
        };
        mock.Setup(m => m.GetAllPlayersAsync()).ReturnsAsync(() => players);
        mock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(true);
        mock.Setup(m => m.GetPlayersByPositionAsync(It.IsAny<PositionGroup>()))
            .ReturnsAsync((PositionGroup pg) => players.Where(pl => pl.Position == pg).ToList());
        mock.Setup(m => m.GetPlayerByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => players.FirstOrDefault(p => p.Id == id));
        mock.Setup(m => m.AddPlayerAsync(It.IsAny<Player>()))
            .Callback(() => { return; });
        mock.Setup(m => m.UpdatePlayer(It.IsAny<Player>()))
           .Callback(() => { return; });
        mock.Setup(m => m.GetAllPlayersCountAsync()).ReturnsAsync(() => players.Count);
        mock.Setup(m => m.ArePlayersOfValidPositionAsync(It.IsAny<PositionGroup>(), It.IsAny<ICollection<int>>()))
            .ReturnsAsync((PositionGroup pg, ICollection<int> pids) => players.All(pl => pl.Position == pg && pids.Contains(pl.Id)));
        mock.Setup(m => m.RemovePlayer(It.IsAny<Player>()))
           .Callback(() => { return; });

        return mock;
    }
}