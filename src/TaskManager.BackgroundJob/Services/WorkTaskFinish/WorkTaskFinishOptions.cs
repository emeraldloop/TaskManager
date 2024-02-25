namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishOptions
{
    public bool IsActive { get; init; }

    public TimeSpan? ExecutionPeriod { get; init; }
}