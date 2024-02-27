using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.Api.Services.WorkTasks;

public class WorkTaskService(
    WorkTaskInteractor workTaskInteractor,
    ISaveRepository saveRepository)
{
    public async Task CreateWorkTaskAsync(CancellationToken cancellationToken)
    {
        await workTaskInteractor.CreateWorkTaskAsync(cancellationToken).ConfigureAwait(false);
        await saveRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task<WorkTask?> GetWorkTaskNullableAsync(Guid workTaskId, CancellationToken cancellationToken)
        => workTaskInteractor.GetWorkTaskNullableAsync(workTaskId, cancellationToken);
}