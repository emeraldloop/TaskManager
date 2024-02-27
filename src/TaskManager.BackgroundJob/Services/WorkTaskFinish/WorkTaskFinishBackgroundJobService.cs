namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishBackgroundJobService(
    WorkTaskFinishOptions workTaskFinishOptions,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<WorkTaskFinishService> logger)
    : BackgroundJobService<WorkTaskFinishService>(workTaskFinishOptions, serviceScopeFactory, logger);