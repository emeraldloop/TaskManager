namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishJobService
    : BackgroundJobService<WorkTaskFinishService>
{
    public WorkTaskFinishJobService(WorkTaskFinishOptions workTaskFinishOptions,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<WorkTaskFinishService> logger)
        : base(workTaskFinishOptions, serviceScopeFactory, logger)
    {
    }
}