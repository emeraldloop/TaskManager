namespace TaskManager.BackgroundJob.Services.WorkTaskFinish;

public class WorkTaskFinishOptions
    : JobServiceOptions
{
    public TimeSpan WorkTaskLifeTime { get; init; }
}