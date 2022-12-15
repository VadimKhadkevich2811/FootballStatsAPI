using AutoMapper;
using FluentAssertions;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Players;
using FootballStats.Application.Players.Handlers;
using FootballStats.UnitTests.MockData.Players;
using FootballStats.UnitTests.MockData.Repositories;
using Moq;

namespace FootballStats.UnitTests.System.Handlers.Players;

public class TestUpdatePlayerDetailHandler
{
    private readonly Mock<IPlayersRepository> _playersRepository;
    private readonly IMapper _mapper;

    public TestUpdatePlayerDetailHandler()
    {
        _playersRepository = IPlayersRepositoryMock.GetMock();

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PlayerMappingProfile());
        });

        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task Handle_PlayerDetailShouldBeUpdated()
    {
        ///Arrange

        var sut = new UpdatePlayerDetailHandler(_playersRepository.Object, _mapper);
        var testPlayer = UpdatePlayerDetailCommandMockData.GetTestUpdatePlayerDetailCommandData();

        /// Act

        var isPlayerUpdated = await sut.Handle(testPlayer, new CancellationToken());

        /// Assert

        isPlayerUpdated!.Data.Should().Be(true);
    }
}