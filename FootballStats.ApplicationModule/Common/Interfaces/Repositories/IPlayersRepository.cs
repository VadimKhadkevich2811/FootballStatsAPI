using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface IPlayersRepository
{
    /// <summary>
    /// Asynchronously adds the player instance to the repository.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> instance that should be added to the repository.</param>
    /// <returns></returns>
    Task AddPlayerAsync(Player player);

    /// <summary>
    /// Removes the player instance from the repository.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> instance that should be removed from the repository.</param>
    /// <returns></returns>
    void RemovePlayer(Player player);

    /// <summary>
    /// Asynchronously returns the player instance by id from the repository.
    /// </summary>
    /// <param name="playerId">The <see cref="System.Int32"/> value that will be used for searching a player in the repository.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a nullable <see cref="Player"/> instance that represents a player
    /// that was found in the repository or was not found.</returns>
    Task<Player?> GetPlayerByIdAsync(int playerId);

    /// <summary>
    /// Asynchronously returns the collection of players from the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Player"/> instances that are stored in the repository.</returns>
    Task<IEnumerable<Player>> GetAllPlayersAsync();

    /// <summary>
    /// Asynchronously returns the collection of players from the repository depending on filter.
    /// </summary>
    /// <param name="playersFilter">The <see cref="PlayersQueryStringParams"/> instance that contains filter and pagination parameters.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a filtered collection of <see cref="Player"/> instances that are stored in the repository.</returns>
    Task<IEnumerable<Player>> GetAllPlayersAsync(PlayersQueryStringParams playersFilter);

    /// <summary>
    /// Asynchronously returns the amount of players in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an <see cref="System.Int32"/> value that represents the amount of players in the repository.</returns>
    Task<int> GetAllPlayersCountAsync();

    /// <summary>
    /// Updates the player instance in the repository.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> instance that should be updated in the repository.</param>
    /// <returns></returns>
    void UpdatePlayer(Player player);

    /// <summary>
    /// Asynchronously defines if specified players are of valid specified coach position.
    /// </summary>
    /// <param name="position">The <see cref="PositionGroup"/> parameter that represents coach position for players validation.</param>
    /// <param name="playerIDs">The collection of <see cref="System.Int32"/> instances that represents IDs of players that should be validated.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a <see cref="System.Boolean"/> instance that represents whether specified players are valid for specified coach position.</returns>
    Task<bool> ArePlayersOfValidPositionAsync(PositionGroup coachPosition, IEnumerable<int> playerIDs);

    /// <summary>
    /// Asynchronously returns the collection of players from the repository that are filtered by specified position.
    /// </summary>
    /// <param name="position">The <see cref="PositionGroup"/> parameter that represents position for players filter.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Player"/> instances with specified position.</returns>
    Task<IEnumerable<Player>> GetPlayersByPositionAsync(PositionGroup position);

    /// <summary>
    /// Asynchronously returns the collection of players from the repository that have trainings on a specified date.
    /// </summary>
    /// <param name="date">The <see cref="System.DateTime"/> parameter that represents date the players have trainings.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Player"/> instances that have trainings on a specified date.</returns>
    Task<IEnumerable<Player>> GetFreePlayersByDateAsync(DateTime date);

    /// <summary>
    /// Asynchronously returns the amount of players that have trainings on a specified date.
    /// </summary>
    /// <param name="date">The <see cref="System.DateTime"/> parameter that represents date the players have trainings.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an amount of <see cref="Player"/> instances that have trainings on a specified date.</returns>
    Task<int> GetFreePlayersByDateCountAsync(DateTime date);

    /// <summary>
    /// Asynchronously returns the collection of players from the repository that are filtered by specified training.
    /// </summary>
    /// <param name="trainingId">The <see cref="System.Int32"/> parameter that represents training id for players filter.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Player"/> instances with specified training.</returns>
    Task<IEnumerable<Player>> GetPlayersByTrainingId(int trainingId);

    /// <summary>
    /// Saves all changes made in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result represents if the state entries are written to the database or not.</returns>
    Task<bool> SaveChangesAsync();
}