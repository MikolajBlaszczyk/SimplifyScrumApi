using DataAccess.Enums.Notification;
using DataAccess.Models.Notifications;

namespace OpenConnectionManagement.Utils;

public class NotificationParameterProvider
{
    public static string GetParameters(Notification notification) 
    {
        return $"Meeting will start in {notification.Advance} minutes.";
            
    }
}