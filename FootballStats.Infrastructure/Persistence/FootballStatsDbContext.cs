using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Infrastructure.Persistence;

public class FootballStatsDbContext : DbContext, IApplicationDbContext
{
    public FootballStatsDbContext(DbContextOptions<FootballStatsDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}