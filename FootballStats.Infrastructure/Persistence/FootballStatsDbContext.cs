using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Infrastructure.Persistence;

public class FootballStatsDbContext : DbContext, IApplicationDbContext
{
    public FootballStatsDbContext(DbContextOptions<FootballStatsDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; } = default!;

    public DbSet<Player> Players { get; set; } = default!;

    public DbSet<Coach> Coaches { get; set; } = default!;

    public DbSet<Training> Trainings { get; set; } = default!;

    public DbSet<TrainingPlayer> TrainingPlayers { get; set; } = default!;

    public async Task<bool> SaveChangesAsync()
    {
        return (await base.SaveChangesAsync() > 0);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coach>()
            .HasOne(t => t.Training)
            .WithOne(c => c.Coach)
            .HasForeignKey<Training>(t => t.CoachId);

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