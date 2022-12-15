using FluentAssertions;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Persistence.Repositories;
using FootballStats.UnitTests.MockData.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FootballStats.UnitTests.System.Repositories;

public class TestSignUpRepository : IDisposable
{
    private readonly FootballStatsDbContext _context;

    public TestSignUpRepository()
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var options = new DbContextOptionsBuilder<FootballStatsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new FootballStatsDbContext(options, mockHttpContextAccessor.Object);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task AddUserAsync_UserIsCreated()
    {
        /// Arrange
        var testUsers = GetUsersMockData.GetAllUsers();
        _context.Users.AddRange(testUsers);
        _context.SaveChanges();
        var testedUser = new User() { Id = 5, Email = "test5@gmail.com", PasswordHash = "hash", Token = "token5", UserName = "un5", UserRole = Role.Admin, TokenEnd = DateTime.Now };

        var sut = new SignUpRepository(_context);

        /// Act

        await sut.AddUserAsync(testedUser);
        await sut.SaveChangesAsync();

        /// Assert

        _context.Users.Should().HaveCount(3);
    }
    [Fact]
    public void UserExists_ReturnTrue()
    {
        /// Arrange
        var testUsers = GetUsersMockData.GetAllUsers();
        _context.Users.AddRange(testUsers);
        _context.SaveChanges();
        var testedUser = new User() { Id = 1, Email = "test1@gmail.com", PasswordHash = "pass1", Token = "token1", UserName = "username1", UserRole = Role.Admin, TokenEnd = DateTime.Now };

        var sut = new SignUpRepository(_context);

        /// Act

        var result = sut.UserExist(testedUser.Email, testedUser.UserName);

        /// Assert

        result.Should().Be(true);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}