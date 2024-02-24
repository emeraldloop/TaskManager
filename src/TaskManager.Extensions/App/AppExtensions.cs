using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using TaskManager.Extensions.DataSource;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TaskManager.Extensions.App;

public static class AppExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment) 
        => services
            .AddServices();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}