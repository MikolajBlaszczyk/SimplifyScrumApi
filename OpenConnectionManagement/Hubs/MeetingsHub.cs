using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OpenConnectionManagement.ConnectionManagement;
using OpenConnectionManagement.Utils;
using SimplifyFramework.Cache;
using SimplifyScrum.Utils;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace OpenConnectionManagement.Hubs;


[Authorize]
public class MeetingsHub(ILogger<MeetingsHub> logger, ICacheKeyValuePairs cache) : Hub
{
    private const HubType Type = HubType.Meetings; 

    public override Task OnConnectedAsync()
    {
        logger.Log(LogLevel.Information,  $"Connected {Context.ConnectionId}");

        var uid = UidProducer.Produce(Type, Context.User!.GetUserGuid());
        cache.Set(uid, Context.ConnectionId);
        
        return base.OnConnectedAsync();
        
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.Log(LogLevel.Information,  $"Disconnected {Context.ConnectionId}");
        
        var uuid = UidProducer.Produce(Type, Context.User!.GetUserGuid());
        cache.Remove(uuid);
        
        return base.OnDisconnectedAsync(exception);
    }
}