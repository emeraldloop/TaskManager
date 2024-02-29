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
    /// Время старта
    /// </summary>
    public DateTime? DateStart { get; protected set; }

    /// <summary>
    /// Время завершения
    /// </summary>
    public DateTime? DateFinished { get; protected set; }

    public WorkTask()
    {
    }

    public WorkTask SetTaskStatus(WorkTaskStatus taskStatus)
    {
        TaskStatus = taskStatus;

        return this;
    }
    
    public WorkTask StartTask(ICurrentTimeProvider currentTimeProvider)
    {
        DateStart = currentTimeProvider.GetNow();
        SetTaskStatus(WorkTaskStatus.Running);

        return this;
    }

    public WorkTask FinishTask(ICurrentTimeProvider currentTimeProvider)
    {
        DateFinished = currentTimeProvider.GetNow();
        SetTaskStatus(WorkTaskStatus.Finished);

        return this;
    }

    public bool IsTimeToFinish(ICurrentTimeProvider currentTimeProvider, TimeSpan workTaskLifeTime)
        => currentTimeProvider.GetNow() - DateCreated >= workTaskLifeTime;
}