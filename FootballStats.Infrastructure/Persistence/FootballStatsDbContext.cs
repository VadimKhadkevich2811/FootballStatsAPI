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

    public DbSet<Coach> Coaches { get; set; }

    public DbSet<Training> Trainings { get; set; }

    public DbSet<TrainingPlayer> TrainingPlayers { get; set; }

    public async Task<bool> SaveChangesAsync()
    {
        return (await base.SaveChangesAsync() > 0);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Training>()
            .HasOne(t => t.Coach)
            .WithOne(c => c.Training)
            .HasForeignKey<Coach>(t => t.TrainingForeignKey);

        modelBuilder.Entity<TrainingPlayer>().HasKey(tp => new { tp.PlayerId, tp.TrainingId });

        modelBuilder.Entity<TrainingPlayer>()
            .HasOne<Player>(tp => tp.Player)
            .WithMany(p => p.TrainingPlayers)
            .HasForeignKey(tp => tp.PlayerId);

        modelBuilder.Entity<TrainingPlayer>()
            .HasOne<Training>(tp => tp.Training)
            .WithMany(t => t.TrainingPlayers)
            .HasForeignKey(tp => tp.TrainingId);
    }
}