using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskInteractor(IWorkTaskRepository workTaskRepository)
{
    public Task<WorkTask> CreateWorkTaskAsync(CancellationToken cancellationToken)
        => workTaskRepository.AddAsync(new WorkTask().SetTaskStatus(WorkTaskStatus.Created), cancellationToken);

    public Task<WorkTask?> GetWorkTaskNullableAsync(Guid workTaskId, CancellationToken cancellationToken)
        => workTaskRepository.GetItemByIdNullableAsync(workTaskId, cancellationToken);
}