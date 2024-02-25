using TaskManager.Domain.Aggregates.Bases.Entities;

namespace TaskManager.Domain.Repositories;

public interface IRepository<TEntity>
    where TEntity : DomainEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity?> GetItemByIdNullableAsync(Guid entityId, CancellationToken cancellationToken);
}