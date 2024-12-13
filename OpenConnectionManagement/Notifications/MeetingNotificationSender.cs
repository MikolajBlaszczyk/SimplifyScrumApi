using DataAccess.Abstraction.Storage;
using DataAccess.Models.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OpenConnectionManagement.Abstraction;
using OpenConnectionManagement.ConnectionManagement;
using OpenConnectionManagement.Hubs;
using OpenConnectionManagement.Utils;
using SimplifyFramework.Cache;

namespace OpenConnectionManagement.Notifications;

public class MeetingNotificationSender(
    IMeetingStorage meetingStorage,
    INotificationStorage notificationStorage,
    IHubContext<MeetingsHub> hubContext,
    ICacheKeyValuePairs cache,
    ILogger<MeetingNotificationSender> logger) : INotificationSender
{
    public async Task<List<Notification>> GatherRequiredNotifications()
    {
        var now = DateTime.Now;
        
        var meetings = await meetingStorage.GetAllMeetings();

        if (meetings.Count == 0)
            return new();
        
        List<Notification> requireToSend = new(); 
        foreach (var meeting in meetings)
        {
            var notifications = await notificationStorage.GetByNotificationSourceGUIDAsync(meeting.GUID);

            var requireToSendForMeeting = notifications
                .Where(n => n.Sent == false && n.Advance <= now.Subtract(meeting.Start).Minutes);
            
            requireToSend.AddRange(requireToSendForMeeting);
        }

        return requireToSend;
    }

    public async Task<bool> SendNotification(Notification notification, string message)
    {
        if(notification.Receivers.Count == 0)
            return false;
        
        string groupName = Guid.NewGuid().ToString();
        foreach(var userGuid in notification.Receivers)
        {
            var connectionID = await GetConnectionIDFromGuid(userGuid);
            if (connectionID is null)
                continue;

            await hubContext.Groups.AddToGroupAsync(connectionID, groupName);
            
        }
        
        
        await hubContext.Clients.Group(groupName).SendAsync(ClientSignalrMethods.IncomingMeeting, message);
        
        notification.Sent = true;
        notificationStorage.UpdateAsync(notification);
        
        return true;
    }
    
    public async Task<string?> GetConnectionIDFromGuid(string userGuid)
    {
        var uid = UidProducer.Produce(HubType.Meetings, userGuid);
        if (cache.Get(uid, out string connectionID))
        {
            return connectionID;
        }
        
        logger.LogError($"Cannot find connectionID for userGuid: {userGuid}. CAnnot send notification!");

        return null;
    }
}