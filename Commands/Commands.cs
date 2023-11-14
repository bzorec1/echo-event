using Discord.Commands;
using EchoEvent.Infrastructure;
using EchoEvent.Models;

namespace EchoEvent.Commands;

public class Commands : ModuleBase<SocketCommandContext>
{
    private static readonly Dictionary<ulong, Event> Events = new Dictionary<ulong, Event>();
    private readonly ITimeProvider _timeProvider;

    public Commands(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    [Command("notifyevent")]
    public async Task NotifyEvent(ulong eventId)
    {
        if (Events.TryGetValue(eventId, out var foundEvent))
        {
            await ReplyAsync($"Notification sent for Event ID {eventId}!");
        }
        else
        {
            await ReplyAsync($"Event with ID {eventId} not found.");
        }
    }

    [Command("rsvp")]
    public async Task Rsvp(ulong eventId, bool attending)
    {
        if (Events.TryGetValue(eventId, out var foundEvent))
        {
            foundEvent.AddRsvp(Context.User.Id, attending);
            await ReplyAsync($"RSVP for Event ID {eventId} recorded. Thank you!");
        }
        else
        {
            await ReplyAsync($"Event with ID {eventId} not found.");
        }
    }

    [Command("waitforresponse")]
    public async Task<string> WaitForResponseWithReminders(ulong eventId, TimeSpan timeout, TimeSpan reminderInterval)
    {
        if (Events.TryGetValue(eventId, out var foundEvent))
        {
            var startTime = _timeProvider.UtcNow();
            var endTime = startTime + timeout;

            while (_timeProvider.UtcNow() < endTime)
            {
                await Task.Delay(reminderInterval);

                // Optionally, you can check for a response during the waiting period
                if (foundEvent.HasResponseFromUser(Context.User.Id))
                {
                    return "Response received!";
                }
            }

            return null; // No response received within the timeout period
        }

        return $"Event with ID {eventId} not found.";
    }
}