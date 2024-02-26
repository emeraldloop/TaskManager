using TaskManager.Domain.Aggregates.WorkTask;

namespace TaskManager.Domain.Repositories.WorkTasks;

public class WorkTaskFilter
    : Filter
{
    public IReadOnlyList<WorkTaskStatus>? WorkTaskStatuses { get; protected set; }

    public WorkTaskFilter SetWorkTaskStatus(WorkTaskStatus workTaskStatus)
        => SetWorkTaskStatuses(new[] { workTaskStatus });

    public WorkTaskFilter SetWorkTaskStatuses(IReadOnlyList<WorkTaskStatus> workTaskStatuses)
    {
        WorkTaskStatuses = workTaskStatuses;

        return this;
    }
}