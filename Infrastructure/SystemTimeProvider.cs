namespace EchoEvent.Infrastructure;

public class SystemTimeProvider : ITimeProvider
{
    public DateTime UtcNow()
    {
        return DateTime.UtcNow;
    }
}