using EchoEvent.Bot.Models;

namespace EchoEvent.Bot.Infrastructure;

public static class EventExtensions
{
    public static void AddRsvp(this Event ev, ulong userId, bool attending)
    {
        ev.RsvPs[userId] = attending;
    }

    public static bool HasResponseFromUser(this Event ev, ulong userId)
    {
        return ev.RsvPs.ContainsKey(userId);
    }
}