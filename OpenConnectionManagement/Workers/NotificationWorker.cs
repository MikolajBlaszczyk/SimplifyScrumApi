using DataAccess.Abstraction.Storage;
using DataAccess.Model.Meetings;
using DataAccess.Models.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenConnectionManagement.Abstraction;
using OpenConnectionManagement.Notifications;
using OpenConnectionManagement.Utils;
using SchedulingModule.Abstraction;
using SchedulingModule.Records;
using SimplifyScrum.Utils;

namespace OpenConnectionManagement.Workers;

        
public class NotificationWorker(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private INotificationSender sender;
    
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        sender = scope.ServiceProvider.GetRequiredService<INotificationSender>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            
            await NotifyAboutIncomingMeetings();
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
        
    }

    //It should be more generic, but for now it's ok
    public async Task NotifyAboutIncomingMeetings()
    {
        var notifications =  await sender.GatherRequiredNotifications();
        
        foreach(var notification in notifications)
        {
            await sender.SendNotification(notification, NotificationParameterProvider.GetParameters(notification), "Incoming Meeting");
        }
    }
}