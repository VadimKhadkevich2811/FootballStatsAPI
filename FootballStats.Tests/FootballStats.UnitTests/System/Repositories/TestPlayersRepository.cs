using FluentAssertions;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Services;
using FootballStats.UnitTests.MockData.Players;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.UnitTests.System.Repositories;

public class TestPlayersRepository : IDisposable
{
    private readonly FootballStatsDbContext _context;

    public TestPlayersRepository()
    {
        var options = new DbContextOptionsBuilder<FootballStatsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new FootballStatsDbContext(options);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAllPlayers_ReturnCollection()
    {
        /// Arrange
        var testPlayers = GetPlayersMockData.GetAllPlayers();
        _context.Players.AddRange(testPlayers);
        _context.SaveChanges();

        var sut = new PlayersRepository(_context, new SortHelper<Player>());

        /// Act

        var result = await sut.GetAllPlayersAsync();

        /// Assert

        result.Should().HaveCount(testPlayers.Count());
    }

    [Fact]
    public async Task GetPlayerById_ShouldReturnCorrectPlayer()
    {
        /// Arrange
        var testPlayers = GetPlayersMockData.GetAllPlayers();
        _context.Players.AddRange(testPlayers);
        _context.SaveChanges();

        var sut = new PlayersRepository(_context, new SortHelper<Player>());

        /// Act

        var result = await sut.GetPlayerByIdAsync(1);

        /// Assert

        result!.Id.Should().Be(1);
    }

    [Fact]
    public async Task AddPlayer_ShouldBeCreatedAndAddedToRepository()
    {
        /// Arrange
        var testPlayers = GetPlayersMockData.GetAllPlayers();
        var newPlayer = new Player() { Id = 4, Name = "Bill", Lastname = "Test4", Age = 25, Position = PositionGroup.Deffender };
        _context.Players.AddRange(testPlayers);
        _context.SaveChanges();

        var sut = new PlayersRepository(_context, new SortHelper<Player>());

        /// Act

        await sut.AddPlayerAsync(newPlayer);
        await sut.SaveChangesAsync();
        var result = await sut.GetAllPlayersCountAsync();

        /// Assert

        result.Should().Be(testPlayers.Count() + 1);
    }

    [Fact]
    public async Task RemovePlayer_ShouldBeRemovedAndDeletedFromRepository()
    {
        /// Arrange
        var testPlayers = GetPlayersMockData.GetAllPlayers();
        var removedPlayer = testPlayers[0];
        _context.Players.AddRange(testPlayers);
        _context.SaveChanges();

        var sut = new PlayersRepository(_context, new SortHelper<Player>());

        /// Act

        sut.RemovePlayer(removedPlayer);
        await sut.SaveChangesAsync();
        var result = await sut.GetAllPlayersCountAsync();

        /// Assert

        result.Should().Be(testPlayers.Count() - 1);
    }

    [Fact]
    public async Task UpdatePlayer_ShouldBeUpdated()
    {
        /// Arrange
        var testPlayers = GetPlayersMockData.GetAllPlayers();
        var updatedPlayer = testPlayers[0];
        _context.Players.AddRange(testPlayers);
        _context.SaveChanges();

        var sut = new PlayersRepository(_context, new SortHelper<Player>());

        /// Act

        updatedPlayer.Age = 50;
        sut.UpdatePlayer(updatedPlayer);
        await sut.SaveChangesAsync();
        var playerToCheck = await sut.GetPlayerByIdAsync(1);

        /// Assert

        playerToCheck!.Age.Should().Be(50);
    }

    [Fact]
    public async Task GetFreePlayersByDate_ReturnCollection()
    {
        /// Arrange
        var testPlayers = GetPlayersMockData.GetAllPlayers();
        _context.Players.AddRange(testPlayers);
        _context.SaveChanges();

        var sut = new PlayersRepository(_context, new SortHelper<Player>());

        /// Act

        var result = await sut.GetFreePlayersByDateAsync(DateTime.Now);

        /// Assert

        result.Should().HaveCount(3);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}