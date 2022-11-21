using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<Player> Players { get; }

    DbSet<Coach> Coaches { get; }

    DbSet<Training> Trainings { get; }

    DbSet<TrainingPlayer> TrainingPlayers { get; }

    Task<bool> SaveChangesAsync();
}