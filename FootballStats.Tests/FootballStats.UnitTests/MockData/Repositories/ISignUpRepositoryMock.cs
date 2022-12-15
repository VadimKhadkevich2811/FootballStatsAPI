using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using Moq;

namespace FootballStats.UnitTests.MockData.Repositories;

public class ISignUpRepositoryMock
{
    public static Mock<ISignUpRepository> GetMock()
    {
        var mock = new Mock<ISignUpRepository>();
        var users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "test@mail.com",
                PasswordHash = "hash",
                Token = "token",
                TokenEnd = DateTime.Now,
                UserName = "name",
                UserRole = Role.Admin
            }
        };
        mock.Setup(m => m.UserExist(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string email, string username) => users.Any(user => user.Email == email
            || user.UserName == username));
        mock.Setup(m => m.AddUserAsync(It.IsAny<User>()))
            .Callback(() => { return; });

        return mock;
    }
}