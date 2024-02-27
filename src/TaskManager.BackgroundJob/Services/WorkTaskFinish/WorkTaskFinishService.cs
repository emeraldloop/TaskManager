using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishService
    : IJobService
{
    private readonly WorkTaskInteractor _workTaskInteractor;
    private readonly WorkTaskFinishOptions _workTaskFinishOptions;

    public WorkTaskFinishService(WorkTaskInteractor workTaskInteractor,
        WorkTaskFinishOptions workTaskFinishOptions)
    {
        _workTaskInteractor = workTaskInteractor;
        _workTaskFinishOptions = workTaskFinishOptions;
    }

    public Task DoJobAsync(CancellationToken cancellationToken)
        => _workTaskInteractor.FinishTasksAsync(_workTaskFinishOptions.WorkTaskLifeTime, cancellationToken);
}