using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.Extensions.Domain;

public static class DomainExtensions
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddInteractors();

    private static IServiceCollection AddInteractors(this IServiceCollection services)
        => services
            .AddScoped<WorkTaskInteractor>();
}