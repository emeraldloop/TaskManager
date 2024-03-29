using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Aggregates.Bases.Entities;
using TaskManager.Domain.Pagination;

namespace TaskManager.DataSource.Pagination;

public class PaginationAsyncEnumerable<TEntity>(IQueryable<TEntity> entities)
    : IAsyncEnumerable<PaginationItem<TEntity>>
    where TEntity : DomainEntity
{
    private const ushort PAGE_SIZE = 10;

    public async IAsyncEnumerator<PaginationItem<TEntity>> GetAsyncEnumerator(
        CancellationToken cancellationToken)
    {
        TEntity? pageLastEntity = null;
        int pageCountEntities;

        do
        {
            var pageItem = await GetPageAscAsync(pageLastEntity, cancellationToken).ConfigureAwait(false);

            pageLastEntity = pageItem.PageLastEntity;
            pageCountEntities = pageItem.PageEntities.Count;

            yield return pageItem;
        } while (pageLastEntity != null && pageCountEntities == PAGE_SIZE);
    }

    private async Task<PaginationItem<TEntity>> GetPageAscAsync(TEntity? lastEntity,
        CancellationToken cancellationToken)
    {
        var pageEntities = await entities
            .OrderBy(x => x.DateCreated)
            .ThenBy(x => x.Id)
            .Where(x =>
                lastEntity == null ||
                x.DateCreated == lastEntity.DateCreated && x.Id.CompareTo(lastEntity.Id) > 0 ||
                x.DateCreated > lastEntity.DateCreated
            )
            .Take(PAGE_SIZE)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PaginationItem<TEntity>(pageEntities);
    }
}