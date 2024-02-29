using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskStartInteractor(
    ICurrentTimeProvider currentTimeProvider,
    IWorkTaskRepository workTaskRepository,
    ISaveRepository saveRepository)
{
    public async Task StartTasksAsync(CancellationToken cancellationToken)
    {
        await foreach (var paginationItem in workTaskRepository
                           .GetAsyncEnumerableByFilter(new WorkTaskFilter()
                                   .SetWorkTaskStatus(WorkTaskStatus.Created),
                               cancellationToken)
                           .ConfigureAwait(false))
        {
            foreach (var workTask in paginationItem.PageEntities)
            {
                workTask.StartTask(currentTimeProvider); // TODO логировать
            }

            await saveRepository.SaveChangesAndClearChangeTrackerAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}