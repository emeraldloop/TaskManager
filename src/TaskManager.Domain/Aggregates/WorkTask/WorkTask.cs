using TaskManager.Domain.Aggregates.Bases.Entities;

namespace TaskManager.Domain.Aggregates.WorkTask;

public class WorkTask
    : DomainEntity
{
    public WorkTaskStatus TaskStatus { get; protected set; }

    protected WorkTask()
    {
    }
}