using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ITrainingsRepository
{
    /// <summary>
    /// Asynchronously adds the training instance to the repository.
    /// </summary>
    /// <param name="training">The <see cref="Training"/> instance that should be added to the repository.</param>
    /// <param name="playerIDs">The collection of <see cref="System.Int32"/> instances that represents player IDs. Players with these IDs will have specified training.</param>
    /// <returns></returns>
    Task AddTrainingAsync(Training training, IEnumerable<int> playerIDs);

    /// <summary>
    /// Removes the training instance from the repository.
    /// </summary>
    /// <param name="training">The <see cref="Training"/> instance that should be removed from the repository.</param>
    /// <returns></returns>
    void RemoveTraining(Training training);

    /// <summary>
    /// Asynchronously returns the training instance by id from the repository.
    /// </summary>
    /// <param name="trainingId">The <see cref="System.Int32"/> value that will be used for searching a training in the repository.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a nullable <see cref="Training"/> instance that represents a training
    /// that was found in the repository or was not found.</returns>
    Task<Training?> GetTrainingByIdAsync(int trainingId);

    /// <summary>
    /// Asynchronously returns the collection of trainings from the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Training"/> instances that are stored in the repository.</returns>
    Task<IEnumerable<Training>> GetAllTrainingsAsync();

    /// <summary>
    /// Asynchronously returns the collection of trainings from the repository depending on filter.
    /// </summary>
    /// <param name="trainingsFilter">The <see cref="TrainingsQueryStringParams"/> instance that contains filter and pagination parameters.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a filtered collection of <see cref="Training"/> instances that are stored in the repository.</returns>
    Task<IEnumerable<Training>> GetAllTrainingsAsync(TrainingsQueryStringParams trainingsFilter);

    /// <summary>
    /// Asynchronously returns the amount of trainings in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an <see cref="System.Int32"/> value that represents the amount of trainings in the repository.</returns>
    Task<int> GetAllTrainingsCountAsync();

    /// <summary>
    /// Updates the training instance in the repository.
    /// </summary>
    /// <param name="training">The <see cref="Training"/> instance that should be updated in the repository.</param>
    /// <param name="playerIDs">The collection of <see cref="System.Int32"/> instances that represents player IDs. Players with these IDs will have specified training.</param>
    /// <returns></returns>
    Task UpdateTrainingAsync(Training training, IEnumerable<int> playerIDs);

    /// <summary>
    /// Asynchronously returns the collection of trainings from the repository that are filtered by specified position.
    /// </summary>
    /// <param name="position">The <see cref="PositionGroup"/> parameter that represents position for trainings filter.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Training"/> instances with specified position.</returns>
    Task<IEnumerable<Training>> GetTrainingsByPositionAsync(PositionGroup position);

    /// <summary>
    /// Asynchronously returns the collection of trainings from the repository that are filtered by specified coach.
    /// </summary>
    /// <param name="coachId">The <see cref="System.Int32"/> parameter that represents coach id for trainings filter.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Training"/> instances with specified coach.</returns>
    Task<IEnumerable<Training>> GetTrainingsByCoachAsync(int coachId);

    /// <summary>
    /// Saves all changes made in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result represents if the state entries are written to the database or not.</returns>
    Task<bool> SaveChangesAsync();
}