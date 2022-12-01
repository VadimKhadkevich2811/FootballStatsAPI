using AutoMapper;
using FluentAssertions;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Mappings;
using FootballStats.ApplicationModule.Players.Handlers;
using FootballStats.UnitTests.MockData.Players;
using FootballStats.UnitTests.MockData.Repositories;
using Moq;

namespace FootballStats.UnitTests.System.Handlers.Players;

public class TestGetAllPlayersHandler
{
    private readonly Mock<IPlayersRepository> _playersRepository;
    private readonly IMapper _mapper;

    public TestGetAllPlayersHandler()
    {
        _playersRepository = IPlayersRepositoryMock.GetMock();

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task Handle_PlayersShouldBeReturned()
    {
        ///Arrange

        var sut = new GetAllPlayersHandler(_playersRepository.Object, _mapper);
        var testPlayers = GetAllPlayersQueryMockData.GetAllPlayersQueryData();

        /// Act

        var players = await sut.Handle(testPlayers, new CancellationToken());

        /// Assert

        players.PlayersTotalCount.Should().Be(await _playersRepository.Object.GetAllPlayersCountAsync());
    }
}