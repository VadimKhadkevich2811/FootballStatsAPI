using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface IApplicationDbContext
{
    /// <summary>
    /// A DbSet of users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// A DbSet of players.
    /// </summary>
    DbSet<Player> Players { get; }

    /// <summary>
    /// A DbSet of coaches.
    /// </summary>
    DbSet<Coach> Coaches { get; }

    /// <summary>
    /// A DbSet of trainings.
    /// </summary>
    DbSet<Training> Trainings { get; }

    /// <summary>
    /// A DbSet of many-to-many trainings/players.
    /// </summary>
    DbSet<TrainingPlayer> TrainingPlayers { get; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result represents if the state entries are written to the database or not.</returns>
    Task<bool> SaveChangesAsync();
}