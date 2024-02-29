using TaskManager.BackgroundJob.Services.WorkTaskFinish;
using TaskManager.BackgroundJob.Services.WorkTaskStart;

namespace TaskManager.BackgroundJob;

public static class ServiceConfiguration
{
    public static IServiceCollection AddBackgroundJobServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var workTaskFinishOptions = configuration
            .GetSection(nameof(WorkTaskFinishOptions))
            .Get<WorkTaskFinishOptions>() ?? new WorkTaskFinishOptions();

        services
            .AddSingleton(workTaskFinishOptions)
            .AddScoped<WorkTaskFinishService>()
            .AddHostedService<WorkTaskFinishBackgroundJobService>();

        var workTaskStartOptions = configuration
            .GetSection(nameof(WorkTaskStartOptions))
            .Get<WorkTaskStartOptions>() ?? new WorkTaskStartOptions();

        services
            .AddSingleton(workTaskStartOptions)
            .AddScoped<WorkTaskStartService>()
            .AddHostedService<WorkTaskStartBackgroundJobService>();

        return services;
    }
}