using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Infrastructure.Persistence;

public class FootballStatsDbContext : DbContext, IApplicationDbContext
{
    public FootballStatsDbContext(DbContextOptions<FootballStatsDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    public DbSet<Player> Players { get; set; }

    public async Task<bool> SaveChangesAsync()
    {
        return (await base.SaveChangesAsync() > 0);
    }
}