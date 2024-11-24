using BacklogModule.Models;
using BacklogModule.Utils;
using DataAccess.Abstraction;
using DataAccess.Enums;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace BacklogModule.Tests;

[TestFixture]
public class BacklogManagerTests
{
    public static Mock<ISprintAccessor> sprintAccessorMock;

    [SetUp]
    public async Task SetUp()
    {
        sprintAccessorMock = new Mock<ISprintAccessor>();
        sprintAccessorMock
            .Setup(a => a.GetSprintInfoByProjectGUID("existingGuid"))
            .Returns(SprintFactory.Create("GUID", "Sprint", "To finish the sprint", 1, DateTime.Now,
                "existingGuid", "", DateTime.Now));

        sprintAccessorMock
            .Setup(a => a.GetSprintInfoByProjectGUID("nonExistingGuid"))
            .Returns(null as Sprint);
    }


    [Test]
    public async Task GetSprintByExistingProjectGUID_ShouldProperlyReturnSprintInfo()
    {
        var backlogManager = new BacklogManager(sprintAccessorMock.Object, null, null);

        var result = backlogManager.GetSprintInfoForProject("existingGuid");

        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public void GetSprintByNonExistingProjectGUID_ShouldProperlyReturnSprintInfo()
    {
        var backlogManager = new BacklogManager(sprintAccessorMock.Object, null, null);

        Assert.ThrowsAsync<BacklogException>( async () =>
        { 
            backlogManager.GetSprintInfoForProject("nonExistingGuid");
        });
    }

    public static IEnumerable<TestCaseData> GetAllProjectData
    {
        get
        {
            var accessor = new Mock<IProjectItemsAccessor>();
            accessor.Setup(a => a.GetAllProjects()).Returns(Task.Run(() => new List<Project>()));
                
            yield return new TestCaseData(
                    accessor, new List<ProjectRecord>()
                );

            var time = DateTime.Now;
            accessor = new Mock<IProjectItemsAccessor>();
            accessor.Setup(a => a.GetAllProjects()).Returns(Task.Run(() => new List<Project>()
            {
                ProjectFactory.Create("1", "abc", null, StandardStatus.New, "searchedTeamGUID", "me", time),
                ProjectFactory.Create("1", "abc", null, StandardStatus.New, "", "me", time)
            }));
                
            yield return new TestCaseData(
                accessor, 
                new List<ProjectRecord>()
                {
                    ProjectRecord.Create("1", "abc", null, StandardStatus.New, "searchedTeamGUID", "me", time)
                }
            );
        }
    }
    
    [Test]
    [TestCaseSource(nameof(GetAllProjectData))]
    public async Task GetProjectsFromDatabase_ShouldReturnAllProjectsSuccessfully(Mock<IProjectItemsAccessor> accessor, List<ProjectRecord> expected)
    {
        var backlogManager = new BacklogManager(null, accessor.Object, null);

        var actual = await backlogManager.GetAllProjectsForTeam("searchedTeamGUID");
        
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    
    public static IEnumerable<TestCaseData> GetFeaturesByGUIDData
    {
        get
        {
            var accessor = new Mock<IProjectItemsAccessor>();
            accessor.Setup(a => a.GetFeatureByProjectGUID("searchedProjectGUID")).Returns(Task.Run(() => new List<Feature>()));
                
            yield return new TestCaseData(
                accessor, new List<FeatureRecord>()
            );

            var time = DateTime.Now;
            accessor = new Mock<IProjectItemsAccessor>();
            accessor.Setup(a => a.GetFeatureByProjectGUID("searchedProjectGUID")).Returns(Task.Run(() => new List<Feature>()
            {
                FeatureFactory.Create("1", "abc", "", ExtendedStatus.New, null, "searchedProjectGUID", "me", time),
                FeatureFactory.Create("2", "abc", "", ExtendedStatus.New, null, "searchedProjectGUID", "me", time)
            }));
                
            yield return new TestCaseData(
                accessor, 
                new List<FeatureRecord>()
                {
                    FeatureRecord.Create("1", "abc", "", ExtendedStatus.New, null, "searchedProjectGUID", "me", time),
                    FeatureRecord.Create("2", "abc", "", ExtendedStatus.New, null, "searchedProjectGUID", "me", time)
                }
            );
        }
    }
    
    [Test]
    [TestCaseSource(nameof(GetFeaturesByGUIDData))]
    public async Task GetFeaturesFromDatabase_ShouldReturnAllProjectsSuccessfully(Mock<IProjectItemsAccessor> accessor, List<FeatureRecord> expected)
    {
        var backlogManager = new BacklogManager(null, accessor.Object, null);

        var actual = await backlogManager.GetAllFeaturesByProjectGUID("searchedProjectGUID");
        
        Assert.That(actual, Is.EquivalentTo(expected));
    }
}