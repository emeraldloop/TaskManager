namespace TaskManager.BackgroundJob.Services.WorkTaskStart;

public class WorkTaskStartBackgroundJobService(
    WorkTaskStartOptions options,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<WorkTaskStartService> logger)
    : BackgroundJobService<WorkTaskStartService>(options, serviceScopeFactory, logger);