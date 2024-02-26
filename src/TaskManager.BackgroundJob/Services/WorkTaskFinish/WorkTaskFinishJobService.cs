using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishJobService
    : BackgroundService
{
    private readonly WorkTaskFinishOptions _workTaskFinishOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<WorkTaskFinishJobService> _logger;

    public WorkTaskFinishJobService(WorkTaskFinishOptions workTaskFinishOptions,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<WorkTaskFinishJobService> logger)
    {
        _workTaskFinishOptions = workTaskFinishOptions;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_workTaskFinishOptions.IsActive)
        {
            return;
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();

                await scope.ServiceProvider
                    .GetRequiredService<WorkTaskInteractor>()
                    .FinishTasksAsync(_workTaskFinishOptions.WorkTaskLifeTime, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger
                    .LogError(ex, "Ошибка при завершении задач в {serviceName}", nameof(WorkTaskFinishJobService));
            }
            finally
            {
                await Task.Delay(_workTaskFinishOptions.ExecutionPeriod, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}