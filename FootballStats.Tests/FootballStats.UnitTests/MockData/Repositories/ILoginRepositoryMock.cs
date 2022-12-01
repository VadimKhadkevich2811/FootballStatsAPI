using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using Moq;

namespace FootballStats.UnitTests.MockData.Repositories;

public class ILoginRepositoryMock
{
    public static Mock<ILoginRepository> GetMock()
    {
        var mock = new Mock<ILoginRepository>();
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
        mock.Setup(m => m.GetUserByEmailOrUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((string loginId) => users.Where(user => user.UserName == loginId ||
                user.Email == loginId).FirstOrDefault());
        mock.Setup(m => m.UpdateUserTokenAsync(It.IsAny<User>(), It.IsAny<string>()))
            .Callback(() => { return; });
        
        return mock;
    }
}