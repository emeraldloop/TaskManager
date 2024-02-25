using TaskManager.Domain.Aggregates.Bases.Entities;
using TaskManager.Domain.Providers.CurrentTime;

namespace TaskManager.Domain.Aggregates.WorkTask;

/// <summary>
/// Задача
/// </summary>
public class WorkTask
    : DomainEntity
{
    /// <summary>
    /// Статус задачи
    /// </summary>
    public WorkTaskStatus TaskStatus { get; protected set; }
    
    /// <summary>
    /// Запланированное время завершения задачи
    /// </summary>
    public DateTime? DateFinishScheduled { get; protected set; }

    protected WorkTask()
    {
    }

    public WorkTask(ICurrentTimeProvider currentTimeProvider, int minutesBeforeFinish)
    {
        DateFinishScheduled = currentTimeProvider.GetNow().AddMinutes(minutesBeforeFinish);
        SetTaskStatus(WorkTaskStatus.Running);
    }


    public WorkTask SetTaskStatus(WorkTaskStatus taskStatus)
    {
        TaskStatus = taskStatus;

        return this;
    }
}