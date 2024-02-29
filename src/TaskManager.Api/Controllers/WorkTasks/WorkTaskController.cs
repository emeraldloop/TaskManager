using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using TaskManager.Api.Services.WorkTasks;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Api.Controllers.WorkTasks;

/// <summary>
/// Работа с задачами
/// </summary>
[ApiController]
[Route("/task")]
public class WorkTaskController(WorkTaskService workTaskService)
    : ControllerBase
{
    /// <summary>
    /// Создать задачу
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var workTask = await workTaskService.CreateWorkTaskAsync(cancellationToken).ConfigureAwait(false);

        return Accepted(workTask.Id);
    }

    /// <summary>
    /// Получить статус задачи
    /// </summary>
    /// <param name="id">id задачи</param>
    [HttpGet("/{id}")]
    public async Task<IActionResult> GetWorkTaskAsync(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            throw new BadRequestException($"Некорректный параметр {nameof(id)}: {id}");
        }

        var workTask = await workTaskService
            .GetWorkTaskAsync(guidId, cancellationToken)
            .ConfigureAwait(false);

        return Ok(workTask.TaskStatus.GetDisplayName());
    }
}