using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Repositories.Save;
using TaskManager.Domain.UseCases.WorkTasks;

namespace TaskManager.Api.Services.WorkTasks;

public class WorkTaskService
{
    private readonly WorkTaskInteractor _workTaskInteractor;
    private readonly ISaveRepository _saveRepository;

    public WorkTaskService(WorkTaskInteractor workTaskInteractor,
        ISaveRepository saveRepository)
    {
        _workTaskInteractor = workTaskInteractor;
        _saveRepository = saveRepository;
    }

    public async Task CreateWorkTaskAsync(CancellationToken cancellationToken)
    {
        await _workTaskInteractor.CreateWorkTaskAsync(cancellationToken).ConfigureAwait(false);
        await _saveRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task<WorkTask?> GetWorkTaskNullableAsync(Guid workTaskId, CancellationToken cancellationToken)
        => _workTaskInteractor.GetWorkTaskNullableAsync(workTaskId, cancellationToken);
}