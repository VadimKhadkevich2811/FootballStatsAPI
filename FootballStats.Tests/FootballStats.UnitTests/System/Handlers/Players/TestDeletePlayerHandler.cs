using AutoMapper;
using FluentAssertions;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Mappings;
using FootballStats.Application.Players.Handlers;
using FootballStats.UnitTests.MockData.Players;
using FootballStats.UnitTests.MockData.Repositories;
using Moq;

namespace FootballStats.UnitTests.System.Handlers.Players;

public class TestDeletePlayerHandler
{
    private readonly Mock<IPlayersRepository> _playersRepository;

    public TestDeletePlayerHandler()
    {
        _playersRepository = IPlayersRepositoryMock.GetMock();
    }

    [Fact]
    public async Task Handle_PlayerShouldBeDeleted()
    {
        ///Arrange

        var sut = new DeletePlayerHandler(_playersRepository.Object);
        var testPlayer = DeletePlayerCommandMockData.GetTestPlayerCommandData();

        /// Act

        var isPlayerDeleted = await sut.Handle(testPlayer, new CancellationToken());

        /// Assert

        isPlayerDeleted!.Data.Should().Be(true);
    }
}