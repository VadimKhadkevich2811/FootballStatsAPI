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
        mock.Setup(m => m.GetAllPlayers()).ReturnsAsync(() => players);
        mock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(true);
        mock.Setup(m => m.GetPlayersByPosition(It.IsAny<PositionGroup>()))
            .ReturnsAsync((PositionGroup pg) => players.Where(pl => pl.Position == pg).ToList());
        mock.Setup(m => m.GetPlayerById(It.IsAny<int>()))
            .ReturnsAsync((int id) => players.FirstOrDefault(p => p.Id == id));
        mock.Setup(m => m.AddPlayer(It.IsAny<Player>()))
            .Callback(() => { return; });
        mock.Setup(m => m.UpdatePlayer(It.IsAny<Player>()))
           .Callback(() => { return; });
        mock.Setup(m => m.GetAllPlayersCount()).ReturnsAsync(() => players.Count);
        mock.Setup(m => m.ArePlayersOfValidPosition(It.IsAny<PositionGroup>()))
            .ReturnsAsync((PositionGroup pg) => players.All(pl => pl.Position == pg));
        mock.Setup(m => m.RemovePlayer(It.IsAny<Player>()))
           .Callback(() => { return; });

        return mock;
    }
}