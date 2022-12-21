using AutoMapper;
using FluentAssertions;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Players;
using FootballStats.Application.Players.Handlers;
using FootballStats.UnitTests.MockData.Players;
using FootballStats.UnitTests.MockData.Repositories;
using Moq;

namespace FootballStats.UnitTests.System.Handlers.Players;

public class TestCreatePlayerHandler
{
    private readonly Mock<IPlayersRepository> _playersRepository;
    private readonly IMapper _mapper;

    public TestCreatePlayerHandler()
    {
        _playersRepository = IPlayersRepositoryMock.GetMock();

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PlayerMappingProfile());
        });

        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task Handle_PlayerShouldBeCreated()
    {
        ///Arrange

        var sut = new CreatePlayerHandler(_playersRepository.Object, _mapper);
        var testPlayer = CreatePlayerCommandMockData.GetTestPlayerCommandData();

        /// Act

        var newPlayer = await sut.Handle(testPlayer, new CancellationToken());

        /// Assert

        newPlayer.Data!.Should().NotBeNull();
        newPlayer.Data!.Name.Should().BeEquivalentTo(testPlayer.Name);
        newPlayer.Data!.Lastname.Should().BeEquivalentTo(testPlayer.Lastname);
        newPlayer.Data!.Age.Should().Be(testPlayer.Age);
        newPlayer.Data!.Position.Should().Be(testPlayer.Position);
    }
}