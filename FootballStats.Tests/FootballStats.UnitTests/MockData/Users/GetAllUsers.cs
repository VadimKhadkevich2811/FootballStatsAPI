using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.UnitTests.MockData.Users;

public class GetUsersMockData
{
    public static List<User> GetAllUsers()
    {
        return new List<User>()
        {
            new() { Id = 1, Email = "test1@gmail.com", PasswordHash="pass1", Token = "token1", UserName = "username1", UserRole=Role.Admin, TokenEnd = DateTime.Now},
            new() { Id = 2, Email = "test2@gmail.com", PasswordHash="pass2", Token = "token2", UserName = "username2", UserRole=Role.User, TokenEnd = DateTime.Now.AddDays(-1)},
        };
    }

}