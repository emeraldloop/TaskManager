using TaskManager.Api.Services.WorkTasks;
using TaskManager.DataSource.Providers.CurrentTime;
using TaskManager.Domain.Providers.CurrentTime;

namespace TaskManager.Api;

public static class ServiceConfiguration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment) 
        => services
            .AddServices();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<WorkTaskService>();
    }
}