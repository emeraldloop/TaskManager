using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.DataSource;
using TaskManager.DataSource.Providers.CurrentTime;
using TaskManager.DataSource.Repositories.Save;
using TaskManager.DataSource.Repositories.WorkTasks;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Extensions.DataSource;

public static class DataSourceExtensions
{
    public static IServiceCollection AddDataSourceLayer(this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddProviders();

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) 
        => services
            .AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(configuration.GetSection("ConnectionStrings")["Database"]);
            });

    public static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IWorkTaskRepository, WorkTaskRepository>()
            .AddScoped<ISaveRepository, SaveRepository>();

    private static IServiceCollection AddProviders(this IServiceCollection services)
        => services
            .AddScoped<ICurrentTimeProvider, CurrentTimeProvider>();
}