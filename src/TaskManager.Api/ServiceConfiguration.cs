using TaskManager.Api.Services.WorkTasks;

namespace TaskManager.Api;

public static class ServiceConfiguration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration) 
        => services
            .AddServices();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<WorkTaskService>();
    }
}