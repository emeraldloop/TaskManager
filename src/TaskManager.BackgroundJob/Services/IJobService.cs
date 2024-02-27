namespace TaskManager.BackgroundJob.Services;

public interface IJobService
{
    Task DoJobAsync(CancellationToken cancellationToken);
}