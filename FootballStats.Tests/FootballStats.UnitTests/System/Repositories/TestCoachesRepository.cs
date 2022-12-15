using FluentAssertions;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Persistence.Repositories;
using FootballStats.Infrastructure.Services;
using FootballStats.UnitTests.MockData.Coaches;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FootballStats.UnitTests.System.Repositories;

public class TestCoachRepository : IDisposable
{
    private readonly FootballStatsDbContext _context;

    public TestCoachRepository()
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var options = new DbContextOptionsBuilder<FootballStatsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new FootballStatsDbContext(options, mockHttpContextAccessor.Object);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAllCoaches_ReturnCollection()
    {
        /// Arrange
        var testCoaches = GetCoachesMockData.GetAllCoaches();
        _context.Coaches.AddRange(testCoaches);
        _context.SaveChanges();

        var sut = new CoachesRepository(_context, new SortHelper<Coach>());

        /// Act

        var result = await sut.GetAllCoachesAsync();

        /// Assert

        result.Should().HaveCount(testCoaches.Count());
    }

    [Fact]
    public async Task GetCoachById_ShouldReturnCorrectPlayer()
    {
        /// Arrange
        var testCoaches = GetCoachesMockData.GetAllCoaches();
        _context.Coaches.AddRange(testCoaches);
        _context.SaveChanges();

        var sut = new CoachesRepository(_context, new SortHelper<Coach>());

        /// Act

        var result = await sut.GetCoachByIdAsync(1);

        /// Assert

        result!.Id.Should().Be(1);
    }

    [Fact]
    public async Task AddCoach_ShouldBeCreatedAndAddedToRepository()
    {
        /// Arrange
        var testCoaches = GetCoachesMockData.GetAllCoaches();
        var newCoach = new Coach()
        {
            Id = 4,
            Name = "Bill",
            Lastname = "Test4",
            Age = 25,
            Nationality = "Argentina",
            Position = PositionGroup.Deffender
        };
        _context.Coaches.AddRange(testCoaches);
        _context.SaveChanges();

        var sut = new CoachesRepository(_context, new SortHelper<Coach>());

        /// Act

        await sut.AddCoachAsync(newCoach);
        await sut.SaveChangesAsync();
        var result = await sut.GetAllCoachesCountAsync();

        /// Assert

        result.Should().Be(testCoaches.Count() + 1);
    }

    [Fact]
    public async Task RemoveCoach_ShouldBeRemovedAndDeletedFromRepository()
    {
        /// Arrange
        var testCoaches = GetCoachesMockData.GetAllCoaches();
        var removedCoach = testCoaches.First();
        _context.Coaches.AddRange(testCoaches);
        _context.SaveChanges();

        var sut = new CoachesRepository(_context, new SortHelper<Coach>());

        /// Act

        sut.RemoveCoach(removedCoach);
        await sut.SaveChangesAsync();
        var result = await sut.GetAllCoachesCountAsync();

        /// Assert

        result.Should().Be(testCoaches.Count() - 1);
    }

    [Fact]
    public async Task UpdateCoach_ShouldBeUpdated()
    {
        /// Arrange
        var testCoaches = GetCoachesMockData.GetAllCoaches();
        var updatedCoach = testCoaches.First();
        _context.Coaches.AddRange(testCoaches);
        _context.SaveChanges();

        var sut = new CoachesRepository(_context, new SortHelper<Coach>());

        /// Act

        updatedCoach.Age = 50;
        sut.UpdateCoach(updatedCoach);
        await sut.SaveChangesAsync();
        var coachToCheck = await sut.GetCoachByIdAsync(1);

        /// Assert

        coachToCheck!.Age.Should().Be(50);
    }

    [Fact]
    public async Task GetFreeCoachesByDate_ReturnCollection()
    {
        /// Arrange
        var testCoaches = GetCoachesMockData.GetAllCoaches();
        _context.Coaches.AddRange(testCoaches);
        _context.SaveChanges();

        var sut = new CoachesRepository(_context, new SortHelper<Coach>());

        /// Act

        var result = await sut.GetFreeCoachesByDateAsync(DateTime.Now);

        /// Assert

        result.Should().HaveCount(3);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}