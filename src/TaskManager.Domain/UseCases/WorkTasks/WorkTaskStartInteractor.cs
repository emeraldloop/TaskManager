using Microsoft.Extensions.Logging;
using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskStartInteractor(
    ICurrentTimeProvider currentTimeProvider,
    IWorkTaskRepository workTaskRepository,
    ISaveRepository saveRepository,
    ILogger<WorkTaskStartInteractor> logger)
{
    public async Task StartTasksAsync(CancellationToken cancellationToken)
    {
        var startedTaskIds = new List<Guid>();

        await foreach (var paginationItem in workTaskRepository
                           .GetAsyncEnumerableByFilter(new WorkTaskFilter()
                                   .SetWorkTaskStatus(WorkTaskStatus.Created),
                               cancellationToken)
                           .ConfigureAwait(false))
        {
            foreach (var workTask in paginationItem.PageEntities)
            {
                workTask.StartTask(currentTimeProvider);
                startedTaskIds.Add(workTask.Id);
            }

            await saveRepository.SaveChangesAndClearChangeTrackerAsync(cancellationToken).ConfigureAwait(false);
        }

        if (startedTaskIds.Count > 0)
        {
            logger.LogInformation("Запущены задачи: " + string.Join(',', startedTaskIds));
        }
    }
}