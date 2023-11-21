namespace EchoEvent.Bot.Models;

public class Event
{
    public ulong EventId { get; set; }
    public Dictionary<ulong, bool> RsvPs { get; set; } = new();
}