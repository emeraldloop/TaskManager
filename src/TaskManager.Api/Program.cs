using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Hosting;
using TaskManager.Api;
using TaskManager.Api.Middlewares.Authorization;
using TaskManager.Api.Middlewares.Exceptions;
using TaskManager.DataSource;
using TaskManager.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;

host
    .ConfigureAppSettings()
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services
            .AddDomainAndDataLayers(configuration)
            .AddApiLayer(configuration)
            .AddControllers();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("public", new OpenApiInfo { Title = "API", Version = "v1" });

            var xmlFilePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskManager.Api.xml")
                }
                .Where(File.Exists);

            foreach (var xmlFilePath in xmlFilePaths)
            {
                options.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
            }

            options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Description = "Basic token Authorization",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "basic"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Basic"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    })
    .UseNLog();

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

    app.UseMiddleware<ExceptionHandlerMiddleware>();
    app.UseMiddleware<TokenAuthorizationMiddleware>();

    app.UseRouting();
    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/public/swagger.json", "API"); });

    using var scope = webApp.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    database.Database.Migrate();
}