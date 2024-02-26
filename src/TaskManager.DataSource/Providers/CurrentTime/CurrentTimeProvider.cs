using TaskManager.Domain.Providers.CurrentTime;

namespace TaskManager.DataSource.Providers.CurrentTime;

public class CurrentTimeProvider
    : ICurrentTimeProvider
{
    private DateTime? _now;

    public DateTime GetNow() => _now ??= DateTime.Now;
}