using FluentAssertions;
using FootballStats.Domain.Entities;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Services;
using FootballStats.UnitTests.MockData.Trainings;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.UnitTests.System.Repositories;

public class TestTrainingsRepository : IDisposable
{
    private readonly FootballStatsDbContext _context;

    public TestTrainingsRepository()
    {
        var options = new DbContextOptionsBuilder<FootballStatsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new FootballStatsDbContext(options);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAllTrainings_ReturnCollection()
    {
        /// Arrange
        var testTrainings = GetTrainingsMockData.GetAllTrainings();
        _context.Trainings.AddRange(testTrainings);
        _context.SaveChanges();

        var sut = new TrainingsRepository(_context, new SortHelper<Training>());

        /// Act

        var result = await sut.GetAllTrainingsAsync();

        /// Assert

        result.Should().HaveCount(testTrainings.Count());
    }

    [Fact]
    public async Task GetTrainingById_ShouldReturnCorrectPlayer()
    {
        /// Arrange
        var testTrainings = GetTrainingsMockData.GetAllTrainings();
        _context.Trainings.AddRange(testTrainings);
        _context.SaveChanges();

        var sut = new TrainingsRepository(_context, new SortHelper<Training>());

        /// Act

        var result = await sut.GetTrainingByIdAsync(1);

        /// Assert

        result!.Id.Should().Be(1);
    }

    [Fact]
    public async Task AddTraining_ShouldBeCreatedAndAddedToRepository()
    {
        /// Arrange
        var testTrainings = GetTrainingsMockData.GetAllTrainings();
        var newTraining = new Training() { Id = 4, Name = "Bill", CoachId = 4 };
        _context.Trainings.AddRange(testTrainings);
        _context.SaveChanges();

        var sut = new TrainingsRepository(_context, new SortHelper<Training>());

        /// Act

        await sut.AddTrainingAsync(newTraining, new List<int>() { 1, 2, 3 });
        await sut.SaveChangesAsync();
        var result = await sut.GetAllTrainingsCountAsync();

        /// Assert

        result.Should().Be(testTrainings.Count() + 1);
    }

    [Fact]
    public async Task RemoveTraining_ShouldBeRemovedAndDeletedFromRepository()
    {
        /// Arrange
        var testTrainings = GetTrainingsMockData.GetAllTrainings();
        var removedTraining = testTrainings[0];
        _context.Trainings.AddRange(testTrainings);
        _context.SaveChanges();

        var sut = new TrainingsRepository(_context, new SortHelper<Training>());

        /// Act

        sut.RemoveTraining(removedTraining);
        await sut.SaveChangesAsync();
        var result = await sut.GetAllTrainingsCountAsync();

        /// Assert

        result.Should().Be(testTrainings.Count() - 1);
    }

    [Fact]
    public async Task UpdateTraining_ShouldBeUpdated()
    {
        /// Arrange
        var testTrainings = GetTrainingsMockData.GetAllTrainings();
        var updatedTraining = testTrainings[0];
        _context.Trainings.AddRange(testTrainings);
        _context.SaveChanges();

        var sut = new TrainingsRepository(_context, new SortHelper<Training>());

        /// Act

        updatedTraining.CoachId = 5;
        await sut.UpdateTrainingAsync(updatedTraining, new List<int>() { 1, 2, 3 });
        await sut.SaveChangesAsync();
        var TrainingToCheck = await sut.GetTrainingByIdAsync(1);

        /// Assert

        TrainingToCheck!.CoachId.Should().Be(5);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}