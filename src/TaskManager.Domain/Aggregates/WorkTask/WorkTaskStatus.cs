using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Aggregates.WorkTask;

/// <summary>
/// Статус задачи
/// </summary>
public enum WorkTaskStatus
{
    /// <summary>
    /// В процессе
    /// </summary>
    [Display(Name = "В процессе")]
    Running,

    /// <summary>
    /// Завершена
    /// </summary>
    [Display(Name = "Завершена")]
    Finished
}