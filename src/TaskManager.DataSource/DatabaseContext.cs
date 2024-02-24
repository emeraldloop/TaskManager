using Microsoft.EntityFrameworkCore;
using TaskManager.DataSource.Configurations.WorkTasks;
using TaskManager.Domain.Aggregates.WorkTask;

namespace TaskManager.DataSource;

public sealed class DatabaseContext
    : DbContext
{
    public DbSet<WorkTask> WorkTasks { get; protected set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new WorkTaskConfiguration());
    }
}