using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<Player> Players { get; }

    Task<bool> SaveChangesAsync();
}