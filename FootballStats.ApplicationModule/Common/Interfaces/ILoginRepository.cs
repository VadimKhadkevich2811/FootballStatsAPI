using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface ILoginRepository
{
    Task<User> GetUserByEmailOrUsername(string loginId);
    Task UpdateUserToken(User user, string token);
    Task<bool> SaveChangesAsync();
}