using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskInteractor(
    ICurrentTimeProvider currentTimeProvider,
    IWorkTaskRepository workTaskRepository,
    ISaveRepository saveRepository)
{
    public Task<WorkTask> CreateWorkTaskAsync(CancellationToken cancellationToken)
        => workTaskRepository.AddAsync(new WorkTask().SetTaskStatus(WorkTaskStatus.Running), cancellationToken);

    public Task<WorkTask?> GetWorkTaskNullableAsync(Guid workTaskId, CancellationToken cancellationToken)
        => workTaskRepository.GetItemByIdNullableAsync(workTaskId, cancellationToken);

    public async Task FinishTasksAsync(TimeSpan workTaskLifeTime, CancellationToken cancellationToken)
    {
        await foreach (var paginationItem in workTaskRepository
                           .GetAsyncEnumerableByFilter(new WorkTaskFilter()
                                   .SetWorkTaskStatus(WorkTaskStatus.Running),
                               cancellationToken))
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