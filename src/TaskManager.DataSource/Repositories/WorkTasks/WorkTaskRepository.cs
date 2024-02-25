using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.DataSource.Repositories.WorkTasks;

public class WorkTaskRepository
    : Repository<WorkTask>, IWorkTaskRepository
{
    public WorkTaskRepository(DatabaseContext db) 
        : base(db)
    {
    }
}