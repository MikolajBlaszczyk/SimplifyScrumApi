using DataAccess.Enums.Notification;
using DataAccess.Models.Notifications;

namespace DataAccess.Abstraction.Storage;

public interface INotificationStorage
{
    Task<Notification> AddAsync(Notification notification);
    Task<Notification> UpdateAsync(Notification notification);
    Task<Notification> DeleteAsync(long id);
    Task<IEnumerable<Notification>?> GetAllAsync();
    Task<IEnumerable<Notification>> GetByNotificationSourceGUIDAsync(string notificationSourceGUIDs);
    Task<IEnumerable<Notification>> GetBySourceTypeAsync(NotificationItem sourceType);
}