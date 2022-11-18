using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ISignUpRepository
{
    bool UserExist(string email, string username);
    Task AddUser(User user);
    Task<bool> SaveChangesAsync();
}