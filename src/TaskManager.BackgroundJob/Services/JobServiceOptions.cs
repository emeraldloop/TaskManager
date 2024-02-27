namespace TaskManager.BackgroundJob.Services;

public abstract class JobServiceOptions
{
    public bool IsActive { get; init; }

    public TimeSpan ExecutionPeriod { get; init; }
}