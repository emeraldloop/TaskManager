namespace TaskManager.Domain.Repositories.Save;

public interface ISaveRepository
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}