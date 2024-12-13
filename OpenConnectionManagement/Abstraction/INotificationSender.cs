using DataAccess.Models.Notifications;

namespace OpenConnectionManagement.Abstraction;

public interface INotificationSender
{
    Task<List<Notification>> GatherRequiredNotifications();
    Task<bool> SendNotification(Notification notification, string message);
    
}