namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishBackgroundJobService
    : BackgroundJobService<WorkTaskFinishService>
{
    public WorkTaskFinishBackgroundJobService(WorkTaskFinishOptions workTaskFinishOptions,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<WorkTaskFinishService> logger)
        : base(workTaskFinishOptions, serviceScopeFactory, logger)
    {
    }
}