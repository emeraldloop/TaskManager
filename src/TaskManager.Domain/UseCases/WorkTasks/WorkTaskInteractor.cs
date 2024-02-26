using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Providers.CurrentTime;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.Domain.UseCases.WorkTasks;

public class WorkTaskInteractor
{
    private readonly ICurrentTimeProvider _currentTimeProvider;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly ISaveRepository _saveRepository;

    public WorkTaskInteractor(ICurrentTimeProvider currentTimeProvider,
        IWorkTaskRepository workTaskRepository, ISaveRepository saveRepository)
    {
        _currentTimeProvider = currentTimeProvider;
        _workTaskRepository = workTaskRepository;
        _saveRepository = saveRepository;
    }

    public Task<WorkTask> CreateWorkTaskAsync(CancellationToken cancellationToken)
        => _workTaskRepository.AddAsync(new WorkTask().SetTaskStatus(WorkTaskStatus.Running), cancellationToken);

    public Task<WorkTask?> GetWorkTaskNullableAsync(Guid workTaskId, CancellationToken cancellationToken)
        => _workTaskRepository.GetItemByIdNullableAsync(workTaskId, cancellationToken);

    public async Task FinishTasksAsync(TimeSpan workTaskLifeTime, CancellationToken cancellationToken)
    {
        await foreach (var paginationItem in _workTaskRepository
                           .GetAsyncEnumerableByFilter(new WorkTaskFilter()
                                   .SetWorkTaskStatus(WorkTaskStatus.Running),
                               cancellationToken))
        {
            foreach (var workTask in paginationItem.PageEntities)
            {
                if (!workTask.IsTimeToFinish(_currentTimeProvider, workTaskLifeTime))
                {
                    continue;
                }

                workTask.FinishTask(_currentTimeProvider);
            }

            await _saveRepository.SaveChangesAndClearChangeTrackerAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}