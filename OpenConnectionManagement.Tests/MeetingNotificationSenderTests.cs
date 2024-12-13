using DataAccess.Abstraction.Storage;
using DataAccess.Model.Meetings;
using DataAccess.Models.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using OpenConnectionManagement.Hubs;
using OpenConnectionManagement.Notifications;
using SimplifyFramework.Cache;

namespace OpenConnectionManagement;

[TestFixture]
public class MeetingNotificationSenderTests
{
    private Mock<IMeetingStorage> _meetingStorageMock;
        private Mock<INotificationStorage> _notificationStorageMock;
        private Mock<IHubContext<MeetingsHub>> _hubContextMock;
        private Mock<ICacheKeyValuePairs> _cacheMock;
        private MeetingNotificationSender _notificationSender;

        [SetUp]
        public void SetUp()
        {
            _meetingStorageMock = new Mock<IMeetingStorage>();
            _notificationStorageMock = new Mock<INotificationStorage>();
            _hubContextMock = new Mock<IHubContext<MeetingsHub>>();
            _cacheMock = new Mock<ICacheKeyValuePairs>();
            _notificationSender = new MeetingNotificationSender(
                _meetingStorageMock.Object,
                _notificationStorageMock.Object,
                _hubContextMock.Object,
                _cacheMock.Object,
                NullLogger<MeetingNotificationSender>.Instance);
        }

        [Test]
        public async Task GatherRequiredNotifications_ShouldReturnEmptyList_WhenNoMeetings()
        {
            _meetingStorageMock.Setup(ms => ms.GetAllMeetings()).ReturnsAsync(new List<Meeting>());

            var result = await _notificationSender.GatherRequiredNotifications();

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GatherRequiredNotifications_ShouldReturnNotifications_WhenMeetingsExist()
        {
            var meeting = new Meeting { GUID = "meeting1", Start = DateTime.Now.AddMinutes(-10) };
            var notification = new Notification { NotificationSourceGUID = "meeting1", Sent = false, Advance = 5 };
            _meetingStorageMock.Setup(ms => ms.GetAllMeetings()).ReturnsAsync(new List<Meeting> { meeting });
            _notificationStorageMock.Setup(ns => ns.GetByNotificationSourceGUIDAsync("meeting1")).ReturnsAsync(new List<Notification> { notification });

            var result = await _notificationSender.GatherRequiredNotifications();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(notification, result.First());
        }

        [Test]
        public async Task SendNotification_ShouldReturnTrue_WhenNotificationSent()
        {
            var notification = new Notification { Receivers = new List<string> { "user1" } };
            _cacheMock.Setup(c => c.Get(It.IsAny<string>(), out It.Ref<string>.IsAny)).Returns(true);
            _hubContextMock.Setup(hc => hc.Groups.AddToGroupAsync(It.IsAny<string>(), It.IsAny<string>(), default)).Returns(Task.CompletedTask);

            var clientProxyMock = new Mock<IClientProxy>();
            clientProxyMock.Setup(cp => cp.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>(), default)).Returns(Task.CompletedTask);

            _hubContextMock.Setup(hc => hc.Clients.Group(It.IsAny<string>())).Returns(clientProxyMock.Object);

            var result = await _notificationSender.SendNotification(notification, "Test message");

            Assert.IsTrue(result);
        }

        [Test]
        public async Task SendNotification_ShouldReturnFalse_WhenNoReceivers()
        {
            var notification = new Notification { Receivers = new List<string>() };
            
            var result = await _notificationSender.SendNotification(notification, "Test message");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetConnectionIDFromGuid_ShouldReturnConnectionID_WhenFoundInCache()
        {
            string connectionID = "connection1";
            _cacheMock.Setup(c => c.Get(It.IsAny<string>(), out connectionID)).Returns(true);

            var result = await _notificationSender.GetConnectionIDFromGuid("user1");

            Assert.AreEqual(connectionID, result);
        }

        [Test]
        public async Task GetConnectionIDFromGuid_ShouldReturnNull_WhenNotFoundInCache()
        {
            string connectionID = null;
            _cacheMock.Setup(c => c.Get(It.IsAny<string>(), out connectionID)).Returns(false);

            var result = await _notificationSender.GetConnectionIDFromGuid("user1");

            Assert.IsNull(result);
        }
}