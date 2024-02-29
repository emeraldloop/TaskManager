using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.BackgroundJob.Services.WorkTaskStart;

public class WorkTaskStartService(WorkTaskStartInteractor workTaskStartInteractor)
    : IJobService
{
    public Task DoJobAsync(CancellationToken cancellationToken)
        => workTaskStartInteractor.StartTasksAsync(cancellationToken);
}