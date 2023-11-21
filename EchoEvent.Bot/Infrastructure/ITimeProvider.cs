namespace EchoEvent.Bot.Infrastructure;

public interface ITimeProvider
{
    DateTime UtcNow();
}