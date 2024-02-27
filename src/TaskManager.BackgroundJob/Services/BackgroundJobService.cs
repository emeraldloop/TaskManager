namespace TaskManager.BackgroundJob.Services;

public abstract class BackgroundJobService<TJobService>(
    JobServiceOptions options,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<TJobService> logger)
    : BackgroundService
    where TJobService : IJobService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var serviceName = GetType().Name;

        if (!options.IsActive)
        {
            logger.LogInformation("Сервис {serviceName} неактивен", serviceName);

            return;
        }

        logger.LogInformation("Запуск сервиса {serviceName}", serviceName);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();

                await scope.ServiceProvider
                    .GetRequiredService<TJobService>()
                    .DoJobAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка в {serviceName}", GetType().Name);
            }
            finally
            {
                await Task.Delay(options.ExecutionPeriod, cancellationToken).ConfigureAwait(false);
            }
        }

        logger.LogInformation("Сервис {serviceName} завершил выполнение", serviceName);
    }
}