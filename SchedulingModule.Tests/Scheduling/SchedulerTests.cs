using DataAccess.Abstraction;
using DataAccess.Accessors;
using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using DataAccess.Models.Factories;
using Microsoft.AspNetCore.Identity;
using Moq;
using SchedulingModule.Enums;
using SchedulingModule.Models;
using SchedulingModule.Records;
using SchedulingModule.Tests.Models;
using SchedulingModule.Tests.Utils;
using SchedulingModule.Utils;
using ScheduleResultFactory = SchedulingModule.Models.Factories.ScheduleResultFactory;

namespace SchedulingModule.Tests.Scheduling;

[TestFixture]
public class SchedulerTests
{
    private static ScheduleTestResult PrepareJanuaryScheduleTestResult()
    {
        var januaryMeetings = new List<Meeting>
        {
            MeetingsFactory.CreateMeetingWithDate("Test1", 1, 1, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test1", 5, 1, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test2", 5, 1, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test1", 10, 1, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test2", 10, 1, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test3", 10, 1, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test4", 10, 1, 2024)
        };
        
        var januaryDayOne = DateTime.Parse("01.01.2024");
        var januaryDayOneMeetings = new List<SimpleMeetingModel>
        {
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test1", januaryDayOne)
        };
        var januaryDayFive = DateTime.Parse("05.01.2024");
        var januaryDayFiveMeetings = new List<SimpleMeetingModel>
        {
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test1", januaryDayFive),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test2", januaryDayFive)
        };
        var januaryDayTen = DateTime.Parse("10.01.2024");
        var januaryDayTenMeetings = new List<SimpleMeetingModel>
        {
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test1", januaryDayTen),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test2", januaryDayTen),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test3", januaryDayTen),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test4", januaryDayTen),
        };
            
        var januaryDays = new List<SimpleDayModel>
        {
            DayFactory.CreateDayRecord(januaryDayOne, januaryDayOneMeetings),
            DayFactory.CreateDayRecord(januaryDayFive, januaryDayFiveMeetings),
            DayFactory.CreateDayRecord(januaryDayTen, januaryDayTenMeetings)
        };
        var januaryScheduleRecord = new SimpleScheduleModel(Month.January, januaryDays);
        var januaryScheduleResult = ScheduleResultFactory.Success(januaryScheduleRecord);
        
        return new ScheduleTestResult(januaryMeetings, januaryScheduleResult);
    }
    
    private static ScheduleTestResult PrepareDecemberScheduleTestResults()
    {
         var decemberMeetings = new List<Meeting>
        {
            MeetingsFactory.CreateMeetingWithDate("Test1", 3, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test2", 3, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test1", 5, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test2", 5, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test1", 10, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test1", 15, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test2", 15, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test3", 15, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test4", 15, 12, 2024),
            MeetingsFactory.CreateMeetingWithDate("Test5", 15, 12, 2024),
        };
        
        var decemberDayThree = DateTime.Parse("03.12.2024");
        var decemberDayThreeMeetings = new List<SimpleMeetingModel>
        {
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test1", decemberDayThree),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test2", decemberDayThree)
        };
        var decemberDayFive = DateTime.Parse("05.12.2024");
        var decemberDayFiveMeetings = new List<SimpleMeetingModel>
        {
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test1", decemberDayFive),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test2", decemberDayFive)
        };
        var decemberDayTen = DateTime.Parse("10.12.2024");
        var decemberDayTenMeetings = new List<SimpleMeetingModel>
        {
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test1", decemberDayTen)
        };
        var decemberDayFifteen = DateTime.Parse("15.12.2024");
        var decemberDayFifteenMeetings = new List<SimpleMeetingModel>
        {
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test1", decemberDayFifteen),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test2", decemberDayFifteen),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test3", decemberDayFifteen),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test4", decemberDayFifteen),
            MeetingsFactory.CreateMeetingRecordWithNameAndStart("Test5", decemberDayFifteen),
        };
            
        var decemberDays = new List<SimpleDayModel>
        {
            DayFactory.CreateDayRecord(decemberDayThree, decemberDayThreeMeetings),
            DayFactory.CreateDayRecord(decemberDayFive, decemberDayFiveMeetings),
            DayFactory.CreateDayRecord(decemberDayTen, decemberDayTenMeetings),
            DayFactory.CreateDayRecord(decemberDayFifteen, decemberDayFifteenMeetings)
        };
        
        var decemberScheduleRecord = new SimpleScheduleModel(Month.December, decemberDays);
        var decemberScheduleResult = ScheduleResultFactory.Success(decemberScheduleRecord);
        
        return new ScheduleTestResult(decemberMeetings, decemberScheduleResult);
    }
    
    public static IEnumerable<TestCaseData> SchedulerRetrievingTestData
    {
        get
        {
            var januaryScheduleResult = PrepareJanuaryScheduleTestResult();
            yield return new TestCaseData(1, 2024, januaryScheduleResult);

            var decemberScheduleResult = PrepareDecemberScheduleTestResults();
            yield return new TestCaseData(12, 2024, decemberScheduleResult);
        }
    }

    [Test]
    [TestCaseSource(nameof(SchedulerRetrievingTestData))]
    public async Task SchedulerGetMeetingsForMonth_ShouldReturnSuccessResult(int month, int year, ScheduleTestResult result)
    {
        var accessorMock = new Mock<IMeetingAccessor>();
        accessorMock.Setup(a => a.GetByMonthAndYearForUserGuid(month, year, It.IsAny<string>())).Returns(result.RawMeetings);
        var converter = new ModelConverter();
        var grouper = new MeetingGrouper(converter);
        var arranger = new CalendarArranger();
        Scheduler scheduler = new Scheduler(accessorMock.Object, grouper, arranger);

        var actual = await scheduler.GetScheduleByMonthForCurrentUser(DateTime.Parse($"01.{month}.{year}"), "");
        
        Assert.IsTrue(actual.IsSuccess);
    }

    
    [Test]
    [TestCaseSource(nameof(SchedulerRetrievingTestData))]
    public async Task SchedulerGetMeetingsForMonth_ShouldReturnProperMonth(int month, int year, ScheduleTestResult result)
    {
        var accessorMock = new Mock<IMeetingAccessor>();
        accessorMock.Setup(a => a.GetByMonthAndYearForUserGuid(month, year, It.IsAny<string>())).Returns(result.RawMeetings);
        var converter = new ModelConverter();
        var grouper = new MeetingGrouper(converter);
        var arranger = new CalendarArranger();
        Scheduler scheduler = new Scheduler(accessorMock.Object, grouper, arranger);

        var actual = await scheduler.GetScheduleByMonthForCurrentUser(DateTime.Parse($"01.{month}.{year}"), "");
        
        Assert.That(actual.ScheduleRecord.month,Is.EqualTo(result.Expected.ScheduleRecord.month));
    }
    
    [Test]
    [TestCaseSource(nameof(SchedulerRetrievingTestData))]
    public async Task SchedulerGetMeetingsForMonth_ShouldReturnProperNumberOfDaysWithMeetings(int month, int year, ScheduleTestResult result)
    {
        var accessorMock = new Mock<IMeetingAccessor>();
        accessorMock.Setup(a => a.GetByMonthAndYearForUserGuid(month, year, "")).Returns(result.RawMeetings);
        var converter = new ModelConverter();
        var grouper = new MeetingGrouper(converter);
        var arranger = new CalendarArranger();
        Scheduler scheduler = new Scheduler(accessorMock.Object, grouper, arranger);

        var actual = await scheduler.GetScheduleByMonthForCurrentUser(DateTime.Parse($"01.{month}.{year}"), "");

        var actualDaysWithMeetings = actual.ScheduleRecord.days.Count(d => d.meetings.Count > 0);
        var expectedDaysWithMeetings = result.Expected.ScheduleRecord.days.Count;
        
        Assert.That(actualDaysWithMeetings, Is.EqualTo(expectedDaysWithMeetings));
    }


    public static IEnumerable<TestCaseData> SchedulerDaysRetrievingTestData
    {
        get
        {
            yield return new TestCaseData(1, 2024, 31);
            yield return new TestCaseData(2, 2024, 29);
            yield return new TestCaseData(2, 2023, 28);
            yield return new TestCaseData(3, 2024, 31);
            yield return new TestCaseData(4, 2024, 30);
            yield return new TestCaseData(5, 2024, 31);
            yield return new TestCaseData(6, 2024, 30);
            yield return new TestCaseData(7, 2024, 31);
            yield return new TestCaseData(8, 2024, 31);
            yield return new TestCaseData(9, 2024, 30);
            yield return new TestCaseData(10, 2024, 31);
            yield return new TestCaseData(11, 2024, 30);
            yield return new TestCaseData(12, 2024, 31);
        }
    }

    [Test]
    [TestCaseSource(nameof(SchedulerDaysRetrievingTestData))]
    public async Task SchedulerGetMeetingsForMonth_ShouldReturnProperNumberOfDaysForMonths(int month, int year,
        int expectedNumberOfDays)
    {
        var accessorMock = new Mock<IMeetingAccessor>();
        accessorMock.Setup(a => a.GetByMonthAndYearForUserGuid(month, year, It.IsAny<string>())).Returns(new List<Meeting>());
        var converter = new ModelConverter();
        var grouper = new MeetingGrouper(converter);
        var arranger = new CalendarArranger();
        Scheduler scheduler = new Scheduler(accessorMock.Object, grouper, arranger);

        var actual = await scheduler.GetScheduleByMonthForCurrentUser(DateTime.Parse($"01.{month}.{year}"), "");

        Assert.That(actual.ScheduleRecord.days.Count, Is.EqualTo(expectedNumberOfDays));
    }

    public static IEnumerable<TestCaseData> MeetingManagerInsertTestData
    {
        get
        {
            var firstGuid = Guid.NewGuid().ToString();
            yield return new TestCaseData(
                    new SimpleMeetingModel(firstGuid, "TestMeeting", "", "",DateTime.Now, TimeSpan.FromHours(2), MeetingType.Custom, new()),
                    MeetingFactory.CreateMeetingWithGuid(firstGuid,"TestMeeting", "", "", DateTime.Now, TimeSpan.FromHours(2), MeetingType.Custom)
                );
        }
    }

    [Test]
    [TestCaseSource(nameof(MeetingManagerInsertTestData))]
    public async Task MeetingManagerInsertMeeting_ShouldSuccessfullyAddNewMeeting(SimpleMeetingModel model, Meeting meeting)
    {
        var accessorMock = new Mock<IMeetingAccessor>();
        accessorMock.Setup(a => a.GetMeetingById(It.IsAny<string>())).Returns((Meeting?)null);
        accessorMock.Setup(a => a.UpsertMeeting(It.IsAny<Meeting>())).Returns(meeting);
        var userStoreMock = new Mock<IUserStore<Teammate>>();
        userStoreMock.Setup(us => us.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Teammate());
        var linker = new TeammateLinker(accessorMock.Object, userStoreMock.Object);
        MeetingManager manager = new MeetingManager(accessorMock.Object, linker);
        
        var actual = await manager.UpsertMeeting(model);
        
        Assert.IsTrue(actual.IsSuccess);
    }

    public static IEnumerable<TestCaseData> MeetingManagerUpdateTestData
    {
        get
        {
            var firstGuid = Guid.NewGuid().ToString();
            var now =DateTime.Now;
            yield return new TestCaseData(
                new SimpleMeetingModel(firstGuid, "TestMeeting2", "456", "a", now, TimeSpan.FromHours(2), MeetingType.Daily, new()),
                MeetingFactory.CreateMeetingWithGuid(firstGuid,"TestMeeting1", "123", "b", DateTime.MinValue, TimeSpan.Zero, MeetingType.Custom),
                MeetingFactory.CreateMeetingWithGuid(firstGuid,"TestMeeting2", "456", "a", now, TimeSpan.FromHours(2), MeetingType.Daily)
                );
        }
    }
    
    [Test]
    [TestCaseSource(nameof(MeetingManagerUpdateTestData))]
    public async Task MeetingManagerUpdateMeeting_ShouldSuccessfullyUpdateMeeting(SimpleMeetingModel model, Meeting meeting, Meeting updatedMeeting)
    {
        var accessorMock = new Mock<IMeetingAccessor>();
        accessorMock.Setup(a => a.GetMeetingById(It.IsAny<string>())).Returns(meeting);
        accessorMock.Setup(a => a.UpsertMeeting(It.IsAny<Meeting>())).Returns(updatedMeeting);
        var userStoreMock = new Mock<IUserStore<Teammate>>();
        userStoreMock.Setup(us => us.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Teammate());
        var linker = new TeammateLinker(accessorMock.Object, userStoreMock.Object);
        MeetingManager manager = new MeetingManager(accessorMock.Object, linker);

        var actual = await manager.UpsertMeeting(model);
        
        Assert.IsTrue(actual.IsSuccess);
    }


    public static IEnumerable<TestCaseData> MeetingManagerDeleteTestData
    {
        get
        {
            var firstGuid = Guid.NewGuid().ToString();
            var now = DateTime.Now;
            yield return new TestCaseData(
                new SimpleMeetingModel(firstGuid, "Test 321", "Some description", "", now, TimeSpan.Zero, MeetingType.Planning, new()),
                MeetingFactory.CreateMeetingWithGuid(firstGuid, "Test 321", "Some description", "", now, TimeSpan.Zero, MeetingType.Planning, new List<TeammateMeetings>())
                );
        }
    }

    [Test]
    [TestCaseSource(nameof(MeetingManagerDeleteTestData))]
    public async Task MeetingManagerDeleteMeeting_ShouldSuccessfullyDeleteMeeting(SimpleMeetingModel model, Meeting expected)
    {
        var accessorMock = new Mock<IMeetingAccessor>();
        accessorMock.Setup(a => a.GetMeetingById(It.IsAny<string>())).Returns(expected);
        accessorMock.Setup(a => a.DeleteMeeting(It.IsAny<Meeting>())).Returns(expected);
        var userStoreMock = new Mock<IUserStore<Teammate>>();
        userStoreMock.Setup(us => us.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Teammate());
        var linker = new TeammateLinker(accessorMock.Object, userStoreMock.Object);
        MeetingManager manager = new MeetingManager(accessorMock.Object, linker);

        var actual = await manager.DeleteMeeting(model);
        
        Assert.IsTrue(actual.IsSuccess);
    }
}