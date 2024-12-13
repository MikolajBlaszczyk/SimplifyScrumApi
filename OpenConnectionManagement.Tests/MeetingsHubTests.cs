

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using SimplfiyTestFramework.IntegrationTestsNewApproach.Hubs;
using SimplifyScrumApi.Tests;

namespace OpenConnectionManagement;

//TODO: Check schemas mechanism to make this integration tests work again.
[TestFixture]
public class MeetingsHubTests
{
    private GenericWebApiFactory _factory;
    private HubConnection _hubConnection;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new GenericWebApiFactory();
        var server = _factory.Server;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("wss://localhost/meetingshub", options =>
            {
                options.HttpMessageHandlerFactory = _ => server.CreateHandler();
            }) 
            .WithAutomaticReconnect()
            .Build();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _hubConnection.DisposeAsync();
        await _factory.DisposeAsync();
    }

    [Test]
    public async Task ConnectToHub_ShouldLogConnection()
    {
        await _hubConnection.StartAsync();
        
        Assert.AreEqual(HubConnectionState.Connected, _hubConnection.State);

        await _hubConnection.StopAsync();
    }

    [Test]
    public async Task DisconnectFromHub_ShouldLogDisconnection()
    {
        await _hubConnection.StartAsync();
        await _hubConnection.StopAsync();

        Assert.AreEqual(HubConnectionState.Disconnected, _hubConnection.State);
    }
}