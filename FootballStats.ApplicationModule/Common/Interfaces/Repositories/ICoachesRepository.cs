using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ICoachesRepository
{
    /// <summary>
    /// Asynchronously adds the coach instance to the repository.
    /// </summary>
    /// <param name="coach">The <see cref="Coach"/> instance that should be added to the repository.</param>
    /// <returns></returns>
    Task AddCoachAsync(Coach coach);

    /// <summary>
    /// Removes the coach instance from the repository.
    /// </summary>
    /// <param name="coach">The <see cref="Coach"/> instance that should be removed from the repository.</param>
    /// <returns></returns>
    void RemoveCoach(Coach coach);

    /// <summary>
    /// Asynchronously returns the coach instance by id from the repository.
    /// </summary>
    /// <param name="coachId">The <see cref="System.Int32"/> value that will be used for searching a coach in the repository.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a nullable <see cref="Coach"/> instance that represents a coach
    /// that was found in the repository or was not found.</returns>
    Task<Coach?> GetCoachByIdAsync(int coachId);

    /// <summary>
    /// Asynchronously returns the collection of coaches from the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Coach"/> instances that are stored in the repository.</returns>
    Task<IEnumerable<Coach>> GetAllCoachesAsync();

    /// <summary>
    /// Asynchronously returns the collection of coaches from the repository depending on filter.
    /// </summary>
    /// <param name="coachesFilter">The <see cref="CoachesQueryStringParams"/> instance that contains filter and pagination parameters.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a filtered collection of <see cref="Coach"/> instances that are stored in the repository.</returns>
    Task<IEnumerable<Coach>> GetAllCoachesAsync(CoachesQueryStringParams coachesFilter);

    /// <summary>
    /// Asynchronously returns the amount of coaches in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an <see cref="System.Int32"/> value that represents the amount of coaches in the repository.</returns>
    Task<int> GetAllCoachesCountAsync();

    /// <summary>
    /// Updates the coach instance in the repository.
    /// </summary>
    /// <param name="coach">The <see cref="Coach"/> instance that should be updated in the repository.</param>
    /// <returns></returns>
    void UpdateCoach(Coach coach);

    /// <summary>
    /// Asynchronously returns the collection of coaches from the repository that are free on a specified date.
    /// </summary>
    /// <param name="date">The <see cref="System.DateTime"/> parameter that represents date the coaches should be free.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Coach"/> instances that are free on a specified date.</returns>
    Task<IEnumerable<Coach>> GetFreeCoachesByDateAsync(DateTime date);

    /// <summary>
    /// Asynchronously returns the amount of coaches that are free on a specified date.
    /// </summary>
    /// <param name="date">The <see cref="System.DateTime"/> parameter that represents date the coaches should be free.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an amount of <see cref="Coach"/> instances that are free on a specified date.</returns>
    Task<int> GetFreeCoachesByDateCountAsync(DateTime date);

    /// <summary>
    /// Saves all changes made in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result represents if the state entries are written to the database or not.</returns>
    Task<bool> SaveChangesAsync();
}