using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishJobService
    : BackgroundService
{
    private readonly WorkTaskFinishOptions _workTaskFinishOptions;
    private readonly WorkTaskInteractor _workTaskInteractor;

    public WorkTaskFinishJobService(WorkTaskFinishOptions workTaskFinishOptions,
        WorkTaskInteractor workTaskInteractor)
    {
        _workTaskFinishOptions = workTaskFinishOptions;
        _workTaskInteractor = workTaskInteractor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_workTaskFinishOptions is { IsActive: true, ExecutionPeriod: not null })
        {
            while (!stoppingToken.IsCancellationRequested)
            {
            }
        }

        return Task.CompletedTask;
    }

    private async Task FinishWorkTasksAsync()
    {
    }
}