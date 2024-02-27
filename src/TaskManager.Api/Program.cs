using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManager.Api;
using TaskManager.DataSource;
using TaskManager.Extensions.Configuration;
using TaskManager.Extensions.DataSource;
using TaskManager.Extensions.Domain;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;

host
    .ConfigureAppSettings()
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddControllers();

        services
            .AddDomainLayer(configuration)
            .AddDataSourceLayer(configuration)
            .AddApiServices(configuration);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("public", new OpenApiInfo { Title = "API", Version = "v1" });

            var xmlFilePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskManager.Api.xml")
                }
                .Where(File.Exists);

            foreach (var xmlFilePath in xmlFilePaths)
            {
                c.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
            }
        });

        services
            .AddDomainLayer(configuration)
            .AddDataSourceLayer(configuration)
            .AddApiServices(configuration)
            ;
    });

var webApp = builder.Build();

ConfigureApp(webApp, webApp.Environment);

webApp.Run();
return;

void ConfigureApp(WebApplication app, IHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();
    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/public/swagger.json", "API"); });

    using var scope = webApp.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    database.Database.Migrate();
}