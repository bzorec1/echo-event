namespace EchoEvent.Infrastructure;

public interface ITimeProvider
{
    DateTime UtcNow();
}