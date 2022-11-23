using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ILoginRepository
{
    Task<User?> GetUserByEmailOrUsernameAsync(string loginId);
    Task UpdateUserTokenAsync(User user, string? token);
    Task<bool> SaveChangesAsync();
}