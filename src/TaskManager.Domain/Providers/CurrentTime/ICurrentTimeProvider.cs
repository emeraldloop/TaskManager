namespace TaskManager.Domain.Providers.CurrentTime;

public interface ICurrentTimeProvider
{
    DateTime GetNow();
}