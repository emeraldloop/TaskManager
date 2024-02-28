using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskFinishInteractor(
    ICurrentTimeProvider currentTimeProvider,
    IWorkTaskRepository workTaskRepository,
    ISaveRepository saveRepository)
{
    public async Task FinishTasksAsync(TimeSpan workTaskLifeTime, CancellationToken cancellationToken)
    {
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
            }

            await saveRepository.SaveChangesAndClearChangeTrackerAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}