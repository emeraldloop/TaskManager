using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Extensions.DataSource;
using TaskManager.Extensions.Domain;

namespace TaskManager.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDomainLayer()
            .AddDataSourceLayer(configuration)
            ;
    }

    public static IHostBuilder ConfigureAppSettings(this IHostBuilder builder)
        => builder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            //TODO настроить конфиги .json
        });

    public static IServiceCollection AddJsonSerializerOptions(this IServiceCollection services,
        Action<JsonSerializerOptions>? configureAction = null)
    {
        var jsonOptions = GetApplicationJsonSerializerOptions(configureAction);
        return services.AddSingleton(jsonOptions);
    }

    public static JsonSerializerOptions GetApplicationJsonSerializerOptions(
        Action<JsonSerializerOptions>? configureAction = null)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        jsonOptions.Converters.Add(new JsonStringEnumConverter());
        configureAction?.Invoke(jsonOptions);

        return jsonOptions;
    }

    public static JsonSerializerOptions GetApplicationJsonSerializerOptions()
    {
        var jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        jsonOptions.Converters.Add(new JsonStringEnumConverter());

        return jsonOptions;
    }
}
