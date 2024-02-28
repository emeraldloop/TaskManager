using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishService(
    WorkTaskFinishInteractor workTaskFinishInteractor,
    WorkTaskFinishOptions workTaskFinishOptions)
    : IJobService
{
    public Task DoJobAsync(CancellationToken cancellationToken)
        => workTaskFinishInteractor.FinishTasksAsync(workTaskFinishOptions.WorkTaskLifeTime, cancellationToken);
}