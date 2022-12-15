using System.Linq.Expressions;
using System.Security.Claims;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Domain.Common;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using FootballStats.Infrastructure.Audition;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Infrastructure.Persistence;

public class FootballStatsDbContext : DbContext, IApplicationDbContext
{
    private readonly IHttpContextAccessor _contextAccessor;
    public FootballStatsDbContext(DbContextOptions<FootballStatsDbContext> options,
        IHttpContextAccessor contextAccessor) : base(options)
    {
        _contextAccessor = contextAccessor;
    }

    public DbSet<User> Users { get; set; } = default!;

    public DbSet<Player> Players { get; set; } = default!;

    public DbSet<Coach> Coaches { get; set; } = default!;

    public DbSet<Training> Trainings { get; set; } = default!;

    public DbSet<TrainingPlayer> TrainingPlayers { get; set; } = default!;

    public DbSet<Audit> AuditLogs { get; set; } = default!;

    public async Task<bool> SaveChangesAsync()
    {
        OnBeforeSaveChanges();
        HandleEntitiesDelete();
        return await base.SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Training>()
            .HasOne(t => t.Coach)
            .WithMany(c => c.Trainings)
            .HasForeignKey(t => t.CoachId);

        modelBuilder.Entity<TrainingPlayer>()
            .HasKey(tp => new { tp.Id, tp.PlayerId, tp.TrainingId });

        modelBuilder.Entity<TrainingPlayer>()
            .HasOne<Player>(tp => tp.Player)
            .WithMany(p => p.TrainingPlayers)
            .HasForeignKey(tp => tp.PlayerId);

        modelBuilder.Entity<TrainingPlayer>()
            .HasOne<Training>(tp => tp.Training)
            .WithMany(t => t.TrainingPlayers)
            .HasForeignKey(tp => tp.TrainingId);

        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(et => typeof(BaseEntity).IsAssignableFrom(et.ClrType));

        foreach (var entityType in entityTypes)
        {
            var isDeletedProperty = entityType.FindProperty("IsDeleted");
            var parameter = Expression.Parameter(entityType.ClrType, "p");
            var filter = Expression.Lambda(
                Expression.Equal(
                    Expression.Property(parameter, isDeletedProperty!.PropertyInfo!),
                        Expression.Constant(false)),
                    parameter);
            entityType.SetQueryFilter(filter);
        }
    }

    private void HandleEntitiesDelete()
    {
        var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);

        foreach (var entity in entities)
        {
            if (entity.Entity is BaseEntity)
            {
                entity.State = EntityState.Modified;
                var changedEntity = entity.Entity as BaseEntity;
                changedEntity!.IsDeleted = true;
            }
        }
    }

    private void OnBeforeSaveChanges()
    {
        if (_contextAccessor.HttpContext == null)
        {
            return;
        }

        ChangeTracker.DetectChanges();

        var userId = _contextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        var auditEntries = new List<AuditEntry>();

        var changedEntries = ChangeTracker.Entries()
            .Where(en => en.Entity is BaseEntity &&
                en.State != EntityState.Detached &&
                en.State != EntityState.Unchanged);

        foreach (var entry in changedEntries)
        {
            var auditEntry = new AuditEntry(entry);

            auditEntry.TableName = entry.Entity.GetType().Name;
            auditEntry.UserId = userId;
            auditEntries.Add(auditEntry);

            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;

                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue!;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue!;
                        break;

                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue!;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = AuditType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue!;
                            auditEntry.NewValues[propertyName] = property.CurrentValue!;
                        }
                        break;
                }
            }
        }

        auditEntries.ForEach(entry => AuditLogs.Add(entry.ToAudit()));
    }
}