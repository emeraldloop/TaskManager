namespace TaskManager.BackgroundJob.Services;

public abstract class BackgroundJobService<TJobService>
    : BackgroundService
    where TJobService : IJobService
{
    private readonly JobServiceOptions _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<TJobService> _logger;

    protected BackgroundJobService(JobServiceOptions options,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<TJobService> logger)
    {
        _options = options;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var serviceName = GetType().Name;

        if (!_options.IsActive)
        {
            _logger.LogInformation("Сервис {serviceName} неактивен", serviceName);

            return;
        }

        _logger.LogInformation("Запуск сервиса {serviceName}", serviceName);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();

                await scope.ServiceProvider
                    .GetRequiredService<TJobService>()
                    .DoJobAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в {serviceName}", GetType().Name);
            }
            finally
            {
                await Task.Delay(_options.ExecutionPeriod, cancellationToken).ConfigureAwait(false);
            }
        }

        _logger.LogInformation("Сервис {serviceName} завершил выполнение", serviceName);
    }
}