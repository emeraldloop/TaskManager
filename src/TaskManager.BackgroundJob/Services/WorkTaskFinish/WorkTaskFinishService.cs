using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishService(
    WorkTaskInteractor workTaskInteractor,
    WorkTaskFinishOptions workTaskFinishOptions)
    : IJobService
{
    public Task DoJobAsync(CancellationToken cancellationToken)
        => workTaskInteractor.FinishTasksAsync(workTaskFinishOptions.WorkTaskLifeTime, cancellationToken);
}