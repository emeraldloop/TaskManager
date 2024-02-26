using Microsoft.EntityFrameworkCore;
using TaskManager.DataSource.Pagination;
using TaskManager.Domain.Aggregates.Bases.Entities;
using TaskManager.Domain.Pagination;
using TaskManager.Domain.Repositories;

namespace TaskManager.DataSource.Repositories;

public abstract class Repository<TEntity, TFilter>
    : IRepository<TEntity, TFilter>
    where TEntity : DomainEntity
    where TFilter : Filter
{
    private readonly DatabaseContext _db;

    protected DbSet<TEntity> DbSet => _db.Set<TEntity>();

    public Repository(DatabaseContext db)
    {
        _db = db;
    }
    
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbSet
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return entity;
    }

    public async Task<TEntity?> GetItemByIdNullableAsync(Guid entityId, CancellationToken cancellationToken)
        => DbSet.Local
            .FirstOrDefault(x => entityId.Equals(x.Id)) ?? await DbSet
            .FirstOrDefaultAsync(x => entityId.Equals(x.Id), cancellationToken)
            .ConfigureAwait(false);
    
    public IAsyncEnumerable<PaginationItem<TEntity>> GetAsyncEnumerableByFilter(TFilter filter,
        CancellationToken cancellationToken)
        => new PaginationAsyncEnumerable<TEntity>(FilterEntities(DbSet, filter));
    
    protected virtual IQueryable<TEntity> FilterEntities(IQueryable<TEntity> entities, TFilter filter) => entities;
}