using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Models.Factories;
using SchedulingModule.Records;
using SchedulingModule.Utils;

namespace SchedulingModule.Tests.Scheduling;

[TestFixture]
public class ModelConverterTests
{

    public static IEnumerable<TestCaseData> ModelConverterTestData
    {
        get
        {
            var firstGuid = Guid.NewGuid().ToString();
            var firstDateTime = DateTime.Now;
            yield return new TestCaseData(
                MeetingFactory.CreateMeetingWithGuid(firstGuid, "Test", "Some description", "", firstDateTime,
                    TimeSpan.FromHours(1), MeetingType.Custom),
                new SimpleMeetingModel(firstGuid, "Test", "Some description", "", firstDateTime, TimeSpan.FromHours(1),
                    MeetingType.Custom, null)
            );

            var secondDateTime = DateTime.Parse("12.09.2002");
            var secondGuid = Guid.NewGuid().ToString();
            yield return new TestCaseData(
                MeetingFactory.CreateMeetingWithGuid(secondGuid, "Test1", "Some description", "", secondDateTime ,
                    TimeSpan.FromHours(4), MeetingType.Daily),
                new SimpleMeetingModel(secondGuid, "Test1", "Some description", "", secondDateTime,
                    TimeSpan.FromHours(4),
                    MeetingType.Daily, null)
            );
        }
    }

    [Test]
    [TestCaseSource(nameof(ModelConverterTestData))]
    public void ModelConverterConvertIntoRecord_ShouldConvertIntoNotNullRecord(Meeting meetingToConvert, SimpleMeetingModel expected)
    {
        var converter = new ModelConverter();

        var actual =converter.ConvertIntoRecord(meetingToConvert);
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [TestCaseSource(nameof(ModelConverterTestData))]
    public void ModelConverterConvertIntoRecord_ShouldProduceRecordThatIsTheSameAsDbModel(Meeting meetingToConvert, SimpleMeetingModel expected)
    {
        var converter = new ModelConverter();

        var actual = converter.ConvertIntoRecord(meetingToConvert);
        
        Assert.AreEqual(actual, expected);
    }
}