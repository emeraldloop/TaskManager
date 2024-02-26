using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Aggregates.Bases.Entities;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;

namespace TaskManager.DataSource.Repositories.Save;

public class SaveRepository
    : ISaveRepository
{
    private readonly ICurrentTimeProvider _currentTimeProvider;
    private DatabaseContext _databaseContext;

    public SaveRepository(ICurrentTimeProvider currentTimeProvider,
        DatabaseContext databaseContext)
    {
        _currentTimeProvider = currentTimeProvider;
        _databaseContext = databaseContext;
    }
    
    public async Task SaveChangesAndClearChangeTrackerAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        _databaseContext.ChangeTracker.Clear();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        CreateEntities();
        UpdateEntities();
        DeleteEntities();

        await _databaseContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    private void CreateEntities()
    {
        var domainEntitiesCreated = _databaseContext.ChangeTracker
            .Entries<DomainEntity>()
            .Where(x => x.State == EntityState.Added);

        foreach (var domainEntity in domainEntitiesCreated)
        {
            domainEntity.Entity.Create(_currentTimeProvider);
        }
    }

    private void UpdateEntities()
    {
        var domainEntitiesUpdated = _databaseContext.ChangeTracker
            .Entries<DomainEntity>()
            .Where(x => x.State == EntityState.Modified);

        foreach (var domainEntity in domainEntitiesUpdated)
        {
            domainEntity.Entity.Update(_currentTimeProvider);
        }
    }
    
    private void DeleteEntities()
    {
        var domainEntitiesDeleted = _databaseContext.ChangeTracker
            .Entries<DomainEntity>()
            .Where(x => x.State == EntityState.Deleted);

        foreach (var domainEntity in domainEntitiesDeleted)
        {
            domainEntity.State = EntityState.Modified;
            domainEntity.Entity.Delete(_currentTimeProvider);
        }
    }
}