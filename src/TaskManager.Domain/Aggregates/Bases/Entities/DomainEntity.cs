namespace TaskManager.Domain.Aggregates.Bases.Entities;

public class DomainEntity
    : IAggregateRoot
{
    public Guid Id { get; }

    public DateTime DateCreated { get; protected set; }

    public DateTime? DateUpdated { get; protected set; }

    public DateTime? DateDeleted { get; protected set; }

    public bool IsDeleted => DateDeleted.HasValue;
}