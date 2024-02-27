using NLog.Extensions.Hosting;
using TaskManager.BackgroundJob;
using TaskManager.Extensions.Configuration;

const string ENVIRONMENT = "DOTNET_ENVIRONMENT";

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppSettings()
    .UseEnvironment(Environment.GetEnvironmentVariable(ENVIRONMENT) ?? Environments.Development)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services
            .AddDomainAndDataLayers(configuration)
            .AddBackgroundJobServices(configuration)
            ;
    })
    .ConfigureLogging((hostBuilderContext, logging) =>
    {
        logging.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));
    })
    .UseNLog()
    .UseSystemd()
    //.UseWindowsService()
    .Build();

host.Run();