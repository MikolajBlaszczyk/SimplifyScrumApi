using DataAccess.Models.Notifications;
using Microsoft.AspNetCore.SignalR;
using OpenConnectionManagement.Abstraction;
using OpenConnectionManagement.Hubs;
using OpenConnectionManagement.Utils;

namespace OpenConnectionManagement.Notifications;

public class TestingNotificationSender(IHubContext<MeetingsHub> hubContext): INotificationSender
{
    public async Task<List<Notification>> GatherRequiredNotifications()
    {
        return new List<Notification>();
    }

    public async Task<bool> SendNotification(Notification notification, string message, string title)
    {
        await hubContext.Clients.All.SendAsync(ClientSignalrMethods.IncomingMeeting, message, Guid.NewGuid().ToString());

        return true;
    }
}