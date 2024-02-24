namespace TaskManager.Domain.Aggregates.WorkTask;

/// <summary>
/// Статус задачи
/// </summary>
public enum WorkTaskStatus
{
    /// <summary>
    /// В процессе
    /// </summary>
    Running,

    /// <summary>
    /// Завершена
    /// </summary>
    Finished,
    
    /// <summary>
    /// Отменена
    /// </summary>
    Terminated
}