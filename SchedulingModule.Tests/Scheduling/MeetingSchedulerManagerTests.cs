using DataAccess.Model.Meetings;
using Microsoft.Extensions.DependencyInjection;
using SchedulingModule.Abstraction;
using SchedulingModule.Records;
using SimplifyScrumApi.Tests;

namespace SchedulingModule.Tests.Scheduling;

[TestFixture]
public class MeetingSchedulerManagerTests
{
    private WebApiFactory factory;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        factory = new WebApiFactory();
        factory.PopulateTestData();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        factory.Dispose();
    }

    [Test]
    [Order(1)]
    public async Task Success_AddMeeting_ShouldCompleteAddAction()
    {
        var manager = factory.Scope.ServiceProvider.GetService<IScheduleMeetings>();
        var meeting = TestData.Meeting;
        
        var result = await manager.AddMeeting(meeting);
        MeetingRecord actual = result.Data;
        
        Assert.IsTrue(actual == meeting);
        Assert.IsTrue(factory.DbContext.Meetings.Contains(meeting));
    }
    
    [Test]
    [Order(2)]
    public async Task Success_UpdateMeeting_ShouldCompleteUpdateAction()
    {
        var manager = factory.Scope.ServiceProvider.GetService<IScheduleMeetings>();
        var meeting = TestData.Meeting;
        meeting.Name = "ABCDE";
        
        var result = await manager.UpdateMeeting(meeting);
        MeetingRecord actual = result.Data;
        
        
        Assert.IsTrue(actual == meeting);
        Assert.IsTrue(factory.DbContext.Meetings.Contains(meeting));
    }
    
    [Test]
    [Order(3)]
    public async Task Success_DeleteMeeting_ShouldDeleteMeeting()
    {
        var manager = factory.Scope.ServiceProvider.GetService<IScheduleMeetings>();
        var meeting = factory.DbContext.Meetings.FirstOrDefault();
        
        var result = await manager.DeleteMeeting(meeting.GUID);
        
        Assert.IsTrue(factory.DbContext.Meetings.Contains(meeting) == false);
    }

    [Test]
    [Order(4)]
    public async Task Success_LinkUser_ShouldLinkUserToMeeting()
    {
        var manager = factory.Scope.ServiceProvider.GetService<IScheduleMeetings>();
        var meeting = TestData.Meeting;
        var user = TestData.User;
        await manager.AddMeeting(meeting);
        
        await manager.LinkUsers(meeting, new List<string>{user.Id});
        
        Assert.IsTrue(factory.DbContext.TeammateMeetings.Count() == 1);
    }
    
    [Test]
    [Order(5)]
    public async Task Success_UnlinkUser_ShouldUnlinkAllUsers()
    {
        var manager = factory.Scope.ServiceProvider.GetService<IScheduleMeetings>();
        var meeting = TestData.Meeting;

        await manager.UnlinkUsers(meeting);
        
        Assert.IsTrue(factory.DbContext.TeammateMeetings.Count() == 0);
    }
}