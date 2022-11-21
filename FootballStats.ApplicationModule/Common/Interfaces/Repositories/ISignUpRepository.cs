using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ISignUpRepository
{
    bool UserExist(string email, string username);
    Task AddUserAsync(User user);
    Task<bool> SaveChangesAsync();
}