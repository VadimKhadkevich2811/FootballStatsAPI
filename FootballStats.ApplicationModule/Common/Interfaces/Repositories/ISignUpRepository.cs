using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ISignUpRepository
{
    /// <summary>
    /// Defines if user with specified email or username exists.
    /// </summary>
    /// <param name="email">The <see cref="System.String"/> instance that represents email for searching the user.</param>
    /// <param name="username">The <see cref="System.String"/> instance that represents username for searching the user.</param>
    /// <returns>A <see cref="System.Boolean"/> instance that determines whether user was found or not.</returns>
    bool UserExist(string email, string username);

    /// <summary>
    /// Asynchronously adds users to the repository.
    /// </summary>
    /// <param name="user">The <see cref="User"/> instance that represents user that should be added to the repository.</param>
    /// <returns></returns>
    Task AddUserAsync(User user);

    /// <summary>
    /// Saves all changes made in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result represents if the state entries are written to the database or not.</returns>
    Task<bool> SaveChangesAsync();
}