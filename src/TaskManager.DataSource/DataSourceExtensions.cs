using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.DataSource.Repositories.WorkTasks;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.DataSource;

public static class DataSourceExtensions
{
    public static IServiceCollection AddDataSourceLayer(this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDatabase(configuration)
            .AddRepositories();

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDbContext<DatabaseContext>(options =>
            {
                //TODO настроить подключение к БД
                options.UseNpgsql("");
            });

    public static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IWorkTaskRepository, WorkTaskRepository>();
}