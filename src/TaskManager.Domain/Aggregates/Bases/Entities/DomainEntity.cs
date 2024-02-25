using TaskManager.Domain.Providers.CurrentTime;

namespace TaskManager.Domain.Aggregates.Bases.Entities;

public abstract class DomainEntity
    : IAggregateRoot
{
    public Guid Id { get; }

    public DateTime DateCreated { get; protected set; }

    public DateTime? DateUpdated { get; protected set; }

    public DateTime? DateDeleted { get; protected set; }

    public bool IsDeleted => DateDeleted.HasValue;
    
    public DomainEntity Create(ICurrentTimeProvider currentTimeProvider)
    {
        DateCreated = currentTimeProvider.GetNow();

        return this;
    }

    public DomainEntity Update(ICurrentTimeProvider currentTimeProvider)
    {
        DateUpdated = currentTimeProvider.GetNow();

        return this;
    }

    public DomainEntity Delete(ICurrentTimeProvider currentTimeProvider)
    {
        DateDeleted = currentTimeProvider.GetNow();

        return this;
    }
}