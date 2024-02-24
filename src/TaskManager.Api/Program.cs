using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManager.DataSource;
using TaskManager.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;

host.ConfigureAppSettings();
ConfigureServices(builder.Services, builder.Configuration);

var webApp = builder.Build();

ConfigureApp(webApp, webApp.Environment);

webApp.Run();
return;

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();
    services.AddAppConfiguration(configuration);

    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("public", new OpenApiInfo { Title = "API", Version = "v1" });

        var xmlFilePaths = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskManager.Domain.xml"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskManager.Api.xml")
            }
            .Where(File.Exists);

        foreach (var xmlFilePath in xmlFilePaths)
        {
            c.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
        }
    });
}

void ConfigureApp(WebApplication app, IHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseRouting();

    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/public/swagger.json", "API"); });

    app.MapControllers();

    using var scope = webApp.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    database.Database.Migrate();
}