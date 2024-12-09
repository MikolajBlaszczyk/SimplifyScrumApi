using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Context;
using DataAccess.Enums.Notification;
using DataAccess.Models.Notifications;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class NotificationStorage(ICreateAccessors accessorsFactory, ILogger<NotificationStorage> logger) : INotificationStorage
{
    private readonly IAccessor<Notification> _accessor = accessorsFactory.Create<Notification>();



    public async Task<Notification> AddAsync(Notification notification)
    {
        var addedNotification = await _accessor.Add(notification);
        if (addedNotification is null)
        {
            logger.LogError("Cannot add notification");
            throw new AccessorException("Cannot add notification");
        }

        return addedNotification;
    }

    public async Task<Notification> UpdateAsync(Notification notification)
    {
        
        var updatedNotification = await _accessor.Update(notification);
        if (updatedNotification is null)
        {
            logger.LogError("Cannot update notification");
            throw new AccessorException("Cannot update notification");
        }

        return updatedNotification;
    }

    public async Task<Notification> DeleteAsync(long id)
    {
        var notification = await _accessor.GetByPK(id);
        if (notification is null)
        {
            logger.LogWarning($"Notification with id {id} does not exist");
            throw new AccessorException($"Notification with id {id} does not exist");
        }

        var deletedNotification = await _accessor.Delete(notification);
        if (deletedNotification is null)
        {
            logger.LogError("Cannot delete notification");
            throw new AccessorException("Cannot delete notification");
        }

        return notification;
    }

    public async Task<IEnumerable<Notification>?> GetAllAsync()
    {
        return await _accessor.GetAll();
    }

    public async Task<IEnumerable<Notification>> GetByNotificationSourceGUIDAsync(string notificationSourceGUID)
    {
        var notification = await GetAllAsync();
        return notification.Where(n => n.NotificationSourceGUID == notificationSourceGUID).ToList();;
    }

    public async Task<IEnumerable<Notification>> GetBySourceTypeAsync(NotificationItem sourceType)
    {  var notification = await GetAllAsync();
        return notification.Where(n => n.SourceType == sourceType).ToList();;
    }
}