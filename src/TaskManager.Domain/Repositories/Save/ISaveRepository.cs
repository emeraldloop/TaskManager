namespace TaskManager.Domain.Repositories.Save;

public interface ISaveRepository
{
    Task SaveChangesAndClearChangeTrackerAsync(CancellationToken cancellationToken);
    
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}