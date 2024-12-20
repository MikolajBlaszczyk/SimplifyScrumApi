using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenConnectionManagement.Abstraction;
using OpenConnectionManagement.Utils;

namespace OpenConnectionManagement.Workers;

public class TestingWorker(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private INotificationSender sender;

    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        sender = scope.ServiceProvider.GetRequiredService<INotificationSender>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await NotifyAboutIncomingMeetings();
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
    
    public async Task NotifyAboutIncomingMeetings()
    {
        await sender.SendNotification(null, "This is test message", "Test title");
    }
}