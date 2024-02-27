using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TaskManager.Extensions.Configuration;

public static class ConfigurationExtensions
{
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
