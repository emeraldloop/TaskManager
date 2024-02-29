using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.Api.Services.WorkTasks;

public class WorkTaskService(
    WorkTaskInteractor workTaskInteractor,
    ISaveRepository saveRepository)
{
    public async Task<WorkTask> CreateWorkTaskAsync(CancellationToken cancellationToken)
    {
        var workTask = await workTaskInteractor.CreateWorkTaskAsync(cancellationToken).ConfigureAwait(false);

        await saveRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return workTask;
    }

    public async Task<WorkTask> GetWorkTaskAsync(Guid workTaskId, CancellationToken cancellationToken)
    {
        var workTask = await workTaskInteractor
            .GetWorkTaskNullableAsync(workTaskId, cancellationToken)
            .ConfigureAwait(false);

        return workTask ?? throw new NotFoundException($"Задачи с Id {workTaskId} нет в системе");
    }
}