using FluentAssertions;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Persistence.Repositories;
using FootballStats.UnitTests.MockData.Users;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.UnitTests.System.Repositories;

public class TestLoginRepository : IDisposable
{
    private readonly FootballStatsDbContext _context;

    public TestLoginRepository()
    {
        var options = new DbContextOptionsBuilder<FootballStatsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new FootballStatsDbContext(options);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetUserByEmailOrUsernameAsync_ReturnUser()
    {
        /// Arrange
        var testUsers = GetUsersMockData.GetAllUsers();
        _context.Users.AddRange(testUsers);
        _context.SaveChanges();

        var sut = new LoginRepository(_context);

        /// Act

        var result = await sut.GetUserByEmailOrUsernameAsync("username1");

        /// Assert

        result!.Id.Should().Be(1);
    }

    [Fact]
    public async Task UpdateUserTokenAsync_TokenIsUpdated()
    {
        /// Arrange
        var testUsers = GetUsersMockData.GetAllUsers();
        _context.Users.AddRange(testUsers);
        _context.SaveChanges();
        var testedUser = testUsers[0];
        var oldTokenEnd = testedUser.TokenEnd;

        var sut = new LoginRepository(_context);

        /// Act

        await sut.UpdateUserTokenAsync(testedUser, "tokenUpdated");

        /// Assert

        testedUser!.Token.Should().Be("tokenUpdated");
        testedUser!.TokenEnd.Should().NotBe(oldTokenEnd);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}