using FootballStats.Domain.Entities;

namespace FootballStats.Application.Common.Interfaces.Repositories;

public interface ILoginRepository
{
    /// <summary>
    /// Asynchronously returns nullable <see cref="User"/> instance by username or email.
    /// </summary>
    /// <param name="loginId">The <see cref="System.String"/> instance that represents username or email by which user should be searched.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a nullable <see cref="User"/> instance that was found by username/email or was not found.</returns>
    Task<User?> GetUserByEmailOrUsernameAsync(string loginId);

    /// <summary>
    /// Updates user token.
    /// </summary>
    /// <param name="user">The <see cref="User"/> instance that represents user whose token should be updated.</param>
    /// <param name="token">The nullable <see cref="System.String"/> instance that represents token that should be set for specified user.</param>
    /// <returns></returns>
    void UpdateUserTokenAsync(User user, string? token);

    /// <summary>
    /// Saves all changes made in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result represents if the state entries are written to the database or not.</returns>
    Task<bool> SaveChangesAsync();
}