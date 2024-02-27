using TaskManager.BackgroundJob.Services.WorkTaskFinish;

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

        return services;
    }
}