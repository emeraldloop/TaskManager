using TaskManager.Domain.Aggregates.Bases.Entities;
using TaskManager.Domain.Pagination;

namespace TaskManager.Domain.Repositories;

public interface IRepository<TEntity, TFilter>
    where TEntity : DomainEntity
    where TFilter : Filter
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity?> GetItemByIdNullableAsync(Guid entityId, CancellationToken cancellationToken);

    public IAsyncEnumerable<PaginationItem<TEntity>> GetAsyncEnumerableByFilter(TFilter filter,
        CancellationToken cancellationToken);
}