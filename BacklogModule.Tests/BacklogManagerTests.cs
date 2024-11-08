using BacklogModule.Utils;
using DataAccess.Abstraction;
using DataAccess.Models.Projects;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace BacklogModule.Tests;

[TestFixture]
public class BacklogManagerTests
{
    public static Mock<ISprintAccessor> accessorMock;

    [SetUp]
    public async Task SetUp()
    {
        accessorMock = new Mock<ISprintAccessor>();
        accessorMock
            .Setup(a => a.GetCurrentSprintInfoByProject("existingGuid"))
            .Returns(new Sprint
            {
                GUID = "GUID",
                Name = "Sprint",
                Goal = "To finish the sprint",
                Iteration = 1,
                End = DateTime.Now,
                ProjectGUID = "existingGuid",
                Project = null,
                SprintNotes = null,
                Creator = "",
                LastUpdate = "",
            });

        accessorMock
            .Setup(a => a.GetCurrentSprintInfoByProject("nonExistingGuid"))
            .Returns(null as Sprint);
    }


    [Test]
    public async Task GetSprintByExistingProjectGUID_ShouldProperlyReturnSprintInfo()
    {
        var backlogManager = new BacklogManager(accessorMock.Object);

        var result = await backlogManager.GetSprintInfoForProject("existingGuid");

        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public void GetSprintByNonExistingProjectGUID_ShouldProperlyReturnSprintInfo()
    {
        var backlogManager = new BacklogManager(accessorMock.Object);

        Assert.ThrowsAsync<BacklogException>( async () =>
        {
            await backlogManager.GetSprintInfoForProject("nonExistingGuid");
        });
    }

}