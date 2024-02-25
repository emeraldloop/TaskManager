using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskInteractor
{
    private readonly ICurrentTimeProvider _currentTimeProvider;
    private readonly IWorkTaskRepository _workTaskRepository;

    public WorkTaskInteractor(ICurrentTimeProvider currentTimeProvider,
        IWorkTaskRepository workTaskRepository)
    {
        _currentTimeProvider = currentTimeProvider;
        _workTaskRepository = workTaskRepository;
    }

    public Task<WorkTask> CreateWorkTaskAsync(int minutesBeforeFinish, CancellationToken cancellationToken)
        => _workTaskRepository.AddAsync(new WorkTask(_currentTimeProvider, minutesBeforeFinish), cancellationToken);

    public Task<WorkTask?> GetWorkTaskNullableAsync(Guid workTaskId, CancellationToken cancellationToken)
        => _workTaskRepository.GetItemByIdNullableAsync(workTaskId, cancellationToken);
}