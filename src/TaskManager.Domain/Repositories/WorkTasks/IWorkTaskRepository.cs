using TaskManager.Domain.Aggregates.WorkTask;

namespace TaskManager.Domain.Repositories.WorkTasks;

public interface IWorkTaskRepository
    : IRepository<WorkTask, WorkTaskFilter>
{
}