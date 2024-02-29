using Microsoft.Extensions.Logging;
using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskFinishInteractor(
    ICurrentTimeProvider currentTimeProvider,
    IWorkTaskRepository workTaskRepository,
    ISaveRepository saveRepository,
    ILogger<WorkTaskFinishInteractor> logger)
{
    public async Task FinishTasksAsync(TimeSpan workTaskLifeTime, CancellationToken cancellationToken)
    {
        var finishedTaskIds = new List<Guid>();

        await foreach (var paginationItem in workTaskRepository
                           .GetAsyncEnumerableByFilter(new WorkTaskFilter()
                                   .SetWorkTaskStatus(WorkTaskStatus.Running),
                               cancellationToken)
                           .ConfigureAwait(false))
        {
            foreach (var workTask in paginationItem.PageEntities)
            {
                if (!workTask.IsTimeToFinish(currentTimeProvider, workTaskLifeTime))
                {
                    continue;
                }

                workTask.FinishTask(currentTimeProvider);
                finishedTaskIds.Add(workTask.Id);
            }

            await saveRepository.SaveChangesAndClearChangeTrackerAsync(cancellationToken).ConfigureAwait(false);
        }

        if (finishedTaskIds.Count > 0)
        {
            logger.LogInformation("Завершены задачи: " + string.Join(',', finishedTaskIds));
        }
    }
}