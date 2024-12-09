using DataAccess.Enums.Notification;
using DataAccess.Models.Notifications;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class NotificationTablesBuilder
{
    public static void BuildNotificationRelatedTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Type)
                .HasDefaultValue(NotificationType.Info);

        });
    }
}