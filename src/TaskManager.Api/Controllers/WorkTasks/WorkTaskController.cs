using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using TaskManager.Api.Services.WorkTasks;

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
    public Task Index(CancellationToken cancellationToken) => workTaskService.CreateWorkTaskAsync(cancellationToken);

    /// <summary>
    /// Получить статус задачи
    /// </summary>
    /// <param name="id">id задачи</param>
    [HttpGet("/{id}")]
    public async Task<IActionResult> GetWorkTaskAsync(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out Guid guidId))
        {
            return BadRequest();
        }

        var workTask = await workTaskService
            .GetWorkTaskNullableAsync(guidId, cancellationToken)
            .ConfigureAwait(false);

        if (workTask == null)
        {
            return NotFound();
        }

        return Ok(workTask.TaskStatus.GetDisplayName());
    }
}