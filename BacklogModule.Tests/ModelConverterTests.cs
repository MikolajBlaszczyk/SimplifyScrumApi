using BacklogModule.Models;
using BacklogModule.Utils;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;

namespace BacklogModule.Tests;

[TestFixture]
public class ModelConverterTests
{
    internal static IEnumerable<TestCaseData> SprintModels
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                    SprintFactory.CreateSprintWithGuid("1","me", "me", "sprint1", "to sprint", 1, time, ""),
                    new SprintRecord("1", "sprint1", "to sprint", 1, time ,"", "me", "me")
                );
        }
    } 
    
    
    [Test]
    [TestCaseSource(nameof(SprintModels))]
    public void ConvertModelToRecords_ModelsShouldBeCorrectlyConverted(Sprint input, SprintRecord expected)
    {
        var actual = ModelConverter.ConvertSprintModelToRecord(input);
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    internal static IEnumerable<TestCaseData> SprintRecords
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                    new SprintRecord("1", "sprint1", "to sprint", 1, time ,"", "me", "me"),
                    SprintFactory.CreateSprintWithGuid("1","me", "me", "sprint1", "to sprint", 1, time, "")
                );
        }
    } 

    [Test]
    [TestCaseSource(nameof(SprintRecords))]
    public void ConvertRecordsToModel_RecordsShouldBeCorrectlyConverted(SprintRecord input, Sprint expected)
    {
        var actual = ModelConverter.ConvertSprintRecordToModel(input);
        
        Assert.IsTrue(actual == expected);
    }
}