using DataAccess.Enums.Notification;
using DataAccess.Models.Notifications;

namespace DataAccess.Models.Factories;

public static class NotificationFactory
{
    public static Notification Create(string notificationSourceGUID, NotificationItem sourceType, NotificationType type, int advance, bool sent, List<string> receivers)
    {
        return new Notification
        {
            NotificationSourceGUID = notificationSourceGUID,
            SourceType = sourceType,
            Type = type,
            Advance = advance,
            Sent = sent,
            Receivers = receivers
        };
    }
}