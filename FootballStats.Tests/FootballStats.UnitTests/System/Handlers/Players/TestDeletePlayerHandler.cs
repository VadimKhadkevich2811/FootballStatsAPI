using AutoMapper;
using FluentAssertions;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Mappings;
using FootballStats.ApplicationModule.Players.Handlers;
using FootballStats.UnitTests.MockData.Players;
using FootballStats.UnitTests.MockData.Repositories;
using Moq;

namespace FootballStats.UnitTests.System.Handlers.Players;

public class TestDeletePlayerHandler
{
    private readonly Mock<IPlayersRepository> _playersRepository;
    private readonly IMapper _mapper;

    public TestDeletePlayerHandler()
    {
        _playersRepository = IPlayersRepositoryMock.GetMock();

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task Handle_PlayerShouldBeDeleted()
    {
        ///Arrange

        var sut = new DeletePlayerHandler(_playersRepository.Object, _mapper);
        var testPlayer = DeletePlayerCommandMockData.GetTestPlayerCommandData();

        /// Act

        var isPlayerDeleted = await sut.Handle(testPlayer, new CancellationToken());

        /// Assert

        isPlayerDeleted.Should().Be(true);
    }
}