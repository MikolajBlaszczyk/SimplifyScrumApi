using BacklogModule.Models;
using DataAccess.Enums;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using Task = DataAccess.Models.Projects.Task;
using TaskFactory = DataAccess.Models.Factories.TaskFactory;

namespace BacklogModule.Tests;

[TestFixture]
public class ModelConversionTests
{
    //Implicit conversion used
    
    #region  Sprint

    internal static IEnumerable<TestCaseData> SprintModels
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                    SprintFactory.Create("1", "sprint1", "to sprint", 1, time, "", "me", time),
                    SprintRecord.Create("1", "sprint1", "to sprint", 1, time ,"", "me", time, "me", time, false)
                );
        }
    } 
    
    
    [Test]
    [TestCaseSource(nameof(SprintModels))]
    public void ConvertSprintModelToRecords_ModelsShouldBeCorrectlyConverted(Sprint input, SprintRecord expected)
    {
        SprintRecord actual =input;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    internal static IEnumerable<TestCaseData> SprintRecords
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                SprintRecord.Create("1", "sprint1", "to sprint", 1, time ,"", "me", time),
                SprintFactory.Create("1", "sprint1", "to sprint", 1, time, "","me", time)
            );
        }
    } 

    [Test]
    [TestCaseSource(nameof(SprintRecords))]
    public void ConvertSprintRecordsToModel_RecordsShouldBeCorrectlyConverted(SprintRecord input, Sprint expected)
    {
        Sprint actual = input;
        
        Assert.IsTrue(actual == expected);
    }
    
    #endregion

    #region Project

    internal static IEnumerable<TestCaseData> ProjectModels
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                ProjectFactory.Create("1", "sprint1", null, StandardStatus.New, "", "me", time),
                ProjectRecord.Create("1", "sprint1", null, StandardStatus.New, "", "me", time)
            );
        }
    } 
    
    
    [Test]
    [TestCaseSource(nameof(ProjectModels))]
    public void ConvertProjectModelToRecords_ModelsShouldBeCorrectlyConverted(Project input, ProjectRecord expected)
    {
        ProjectRecord actual = input;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    
    internal static IEnumerable<TestCaseData> ProjectRecords
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                ProjectRecord.Create("1", "Project", null, StandardStatus.New, "Some", "me", time),
                ProjectFactory.Create("1", "Project", null, StandardStatus.New, "Some", "me", time)
            );
        }
    } 

    [Test]
    [TestCaseSource(nameof(ProjectRecords))]
    public void ConvertProjectRecordsToModel_RecordsShouldBeCorrectlyConverted(ProjectRecord input, Project expected)
    {
        Project actual = input;
        
        Assert.IsTrue(actual == expected);
    }


    #endregion

    #region Feature

    internal static IEnumerable<TestCaseData> FeatureModels
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                FeatureFactory.Create("1", "feature 1", "no desc", ExtendedStatus.New, null, "", "me", time),
                FeatureRecord.Create("1", "feature 1", "no desc", ExtendedStatus.New, null, "", "me", time)
            );
        }
    } 
    
    
    [Test]
    [TestCaseSource(nameof(FeatureModels))]
    public void ConvertFeatureModelToRecords_ModelsShouldBeCorrectlyConverted(Feature input, FeatureRecord expected)
    {
        FeatureRecord actual = input;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    
    internal static IEnumerable<TestCaseData> FeatureRecords
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                FeatureRecord.Create("1", "feature 1", "no desc", ExtendedStatus.New, null, "", "me", time),
                FeatureFactory.Create("1", "feature 1", "no desc", ExtendedStatus.New, null, "", "me", time)
            );
        }
    } 

    [Test]
    [TestCaseSource(nameof(FeatureRecords))]
    public void ConvertFeatureRecordsToModel_RecordsShouldBeCorrectlyConverted(FeatureRecord input, Feature expected)
    {
        Feature actual = input;
        
        Assert.IsTrue(actual == expected);
    }


    #endregion
    
    #region Task

    internal static IEnumerable<TestCaseData> TaskModels
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                TaskFactory.Create(1, "task", SimpleStatus.ToBeDone, "", null, "me", time),
                TaskRecord.Create(1, "task", SimpleStatus.ToBeDone, "", null, "me", time)
            );
        }
    } 
    
    
    [Test]
    [TestCaseSource(nameof(TaskModels))]
    public void ConvertTaskModelToRecords_ModelsShouldBeCorrectlyConverted(Task input, TaskRecord expected)
    {
        TaskRecord actual = input;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    
    internal static IEnumerable<TestCaseData> TaskRecords
    {
        get
        {
            DateTime time = DateTime.Now;
            yield return new TestCaseData(
                TaskRecord.Create(1, "task", SimpleStatus.ToBeDone, "", null, "me", time),
                TaskFactory.Create(1, "task", SimpleStatus.ToBeDone, "", null, "me", time)
            );
        }
    } 

    [Test]
    [TestCaseSource(nameof(TaskRecords))]
    public void ConvertTaskRecordsToModel_RecordsShouldBeCorrectlyConverted(TaskRecord input, Task expected)
    {
        Task actual = input;
        
        Assert.IsTrue(actual == expected);
    }


    #endregion
}