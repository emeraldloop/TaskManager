using TaskManager.Api.Services.WorkTasks;

namespace TaskManager.Api;

public static class ServiceConfiguration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration) 
        => services
            .AddScoped<WorkTaskService>();
}