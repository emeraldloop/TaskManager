using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using TaskManager.Api.Services.WorkTasks;

namespace TaskManager.Api.Controllers.WorkTasks;

[ApiController]
[Route("/task")]
public class WorkTaskController
    : ControllerBase
{
    private readonly WorkTaskService _workTaskService;

    public WorkTaskController(WorkTaskService workTaskService)
    {
        _workTaskService = workTaskService;
    }

    [HttpPost]
    public Task Index(CancellationToken cancellationToken) => _workTaskService.CreateWorkTaskAsync(cancellationToken);

    [HttpGet("/{id}")]
    public async Task<IActionResult> GetWorkTaskAsync(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out Guid guidId))
        {
            return BadRequest();
        }

        var workTask = await _workTaskService
            .GetWorkTaskNullableAsync(guidId, cancellationToken)
            .ConfigureAwait(false);

        if (workTask == null)
        {
            return NotFound();
        }

        return Ok(workTask.TaskStatus.GetDisplayName());
    }
}