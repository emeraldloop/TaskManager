using TaskManager.Domain.Aggregates.WorkTask;
using TaskManager.Domain.Repositories.WorkTasks;

namespace TaskManager.DataSource.Repositories.WorkTasks;

public class WorkTaskRepository
    : Repository<WorkTask, WorkTaskFilter>, IWorkTaskRepository
{
    public WorkTaskRepository(DatabaseContext db)
        : base(db)
    {
    }

    protected override IQueryable<WorkTask> FilterEntities(IQueryable<WorkTask> entities, WorkTaskFilter filter)
    {
        if (filter.WorkTaskStatuses != null)
        {
            entities = entities.Where(x => filter.WorkTaskStatuses.Contains(x.TaskStatus));
        }

        return base.FilterEntities(entities, filter);
    }
}