using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Aggregates.Bases.Entities;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;

namespace TaskManager.DataSource.Repositories.Save;

public class SaveRepository(
    ICurrentTimeProvider currentTimeProvider,
    DatabaseContext databaseContext)
    : ISaveRepository
{
    public async Task SaveChangesAndClearChangeTrackerAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        databaseContext.ChangeTracker.Clear();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        CreateEntities();
        UpdateEntities();
        DeleteEntities();

        await databaseContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void CreateEntities()
    {
        var domainEntitiesCreated = databaseContext.ChangeTracker
            .Entries<DomainEntity>()
            .Where(x => x.State == EntityState.Added);

        foreach (var domainEntity in domainEntitiesCreated)
        {
            domainEntity.Entity.Create(currentTimeProvider);
        }
    }

    private void UpdateEntities()
    {
        var domainEntitiesUpdated = databaseContext.ChangeTracker
            .Entries<DomainEntity>()
            .Where(x => x.State == EntityState.Modified);

        foreach (var domainEntity in domainEntitiesUpdated)
        {
            domainEntity.Entity.Update(currentTimeProvider);
        }
    }

    private void DeleteEntities()
    {
        var domainEntitiesDeleted = databaseContext.ChangeTracker
            .Entries<DomainEntity>()
            .Where(x => x.State == EntityState.Deleted);

        foreach (var domainEntity in domainEntitiesDeleted)
        {
            domainEntity.State = EntityState.Modified;
            domainEntity.Entity.Delete(currentTimeProvider);
        }
    }
}