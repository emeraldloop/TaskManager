using TaskManager.Domain.Aggregates.Bases.Entities;

namespace TaskManager.Domain.Pagination;

public class PaginationItem<TEntity>
    where TEntity : DomainEntity
{
    public TEntity? PageLastEntity { get; }

    public IReadOnlyList<TEntity> PageEntities { get; }

    public PaginationItem(IReadOnlyList<TEntity> pageEntities)
    {
        PageLastEntity = pageEntities.LastOrDefault();
        PageEntities = pageEntities;
    }
}