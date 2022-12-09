using AutoMapper;
using FluentAssertions;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Mappings;
using FootballStats.ApplicationModule.Common.Players.Handlers;
using FootballStats.ApplicationModule.Players.Handlers;
using FootballStats.UnitTests.MockData.Players;
using FootballStats.UnitTests.MockData.Repositories;
using Moq;

namespace FootballStats.UnitTests.System.Handlers.Players;

public class TestUpdatePlayerHandler
{
    private readonly Mock<IPlayersRepository> _playersRepository;
    private readonly IMapper _mapper;

    public TestUpdatePlayerHandler()
    {
        _playersRepository = IPlayersRepositoryMock.GetMock();

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task Handle_PlayerShouldBeUpdated()
    {
        ///Arrange

        var sut = new UpdatePlayerHandler(_playersRepository.Object, _mapper);
        var testPlayer = UpdatePlayerCommandMockData.GetTestUpdatePlayerCommandData();

        /// Act

        var isPlayerUpdated = await sut.Handle(testPlayer, new CancellationToken());

        /// Assert

        isPlayerUpdated!.Data.Should().Be(true);
    }
}