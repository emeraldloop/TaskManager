using TaskManager.Api.Middlewares.Authorization;
using TaskManager.Api.Services.WorkTasks;

namespace TaskManager.Api;

public static class ServiceConfiguration
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration) 
        => services
            .AddServices(configuration)
            .AddOptions(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddScoped<WorkTaskService>();

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration
            .GetSection(nameof(AuthorizationOptions))
            .Get<AuthorizationOptions>() ?? new AuthorizationOptions();

        services.AddSingleton(authOptions);

        return services;
    }
}