using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Extensions.DataSource;
using TaskManager.Extensions.Domain;

namespace TaskManager.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddDomainAndDataLayers(this IServiceCollection services, 
        IConfiguration configuration)
        => services
            .AddDomainLayer(configuration)
            .AddDataSourceLayer(configuration);
    
    public static IHostBuilder ConfigureAppSettings(this IHostBuilder builder)
        => builder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            var environment = hostingContext.HostingEnvironment;

            var basePath = string.Empty;
            if (environment.IsDevelopment())
            {
                basePath = Path.Combine(environment.ContentRootPath, "..",
                    typeof(ConfigurationExtensions).Assembly.GetName().Name ?? string.Empty);
            }

            config
                .AddJsonFile(Path.Combine(basePath, "appsettings.json"), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine(basePath, $"appsettings.{environment.EnvironmentName}.json"), optional: true,
                    reloadOnChange: true)
                ;
        });
}
