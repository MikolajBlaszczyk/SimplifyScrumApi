using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.BacklogManagement;
using BacklogModule.Models;
using DataAccess.Abstraction.Storage;
using DataAccess.Enums;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework.Internal;
using SimplifyScrum.Utils;
using SimplifyScrumApi.Tests;
using Task = System.Threading.Tasks.Task;
using TaskFactory = System.Threading.Tasks.TaskFactory;

namespace BacklogModule.Tests.Management;


[TestFixture]
[Ignore("Rewrite Sprint tests")]
public class SprintManagementTests
{
    private  WebApiFactory factory;
    private Mock<ISprintStorage> sprintStorageMock;
    private Mock<IFeatureStorage> featureStorageMock;

    [SetUp]
    public void Setup()
    {
        sprintStorageMock = new Mock<ISprintStorage>();
        featureStorageMock = new Mock<IFeatureStorage>();
    }
    
    [Test]
    public async Task Success_GetProjectSprint_ShouldGetNotNullValue()
    {
        
        var manager = GetManager<IManageSprint>(factory.Scope);
        var sprint = factory.DbContext.Sprints.FirstOrDefault(s => s.ProjectGUID.IsNullOrEmpty() == false);
        var project = factory.DbContext.Projects.FirstOrDefault(p => p.GUID == sprint.ProjectGUID);
    
        var result = await manager.GetSprintInfoByProjectGUID(project.GUID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    public async Task Success_GetProjectSprint_NonExisting_ShouldReturnNull()
    {
        var storage =new Mock<ISprintStorage>();
        var featureStorage =new Mock<IFeatureStorage>();
        storage.Setup(s => s.AddSprint(It.IsAny<Sprint>())).ReturnsAsync(new Sprint());
        storage.Setup(s => s.LinkSprintWithFeatures(It.IsNotNull<Sprint>(), It.IsNotNull<List<string>>()));
        var manager = new SprintManager(storage.Object, featureStorage.Object, null, null);
        
        var result = await manager.GetSprintInfoByProjectGUID("");
        var actual = result.Data;
        
        Assert.IsNull(actual);
    }

    [Test]
    public async Task Success_PlanSprint_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageSprint>(factory.Scope);

        var features = TestData.Features_Success_PlanSprint_ShouldReturnNotNull.Select(f => f.GUID).ToList();
        var plan = new PlanSprintRecord(TestData.Sprint_Success_PlanSprint_ShouldReturnNotNull, features);
        
        var result = await manager.PlanSprint(plan);
        var actual = result.Data as SprintRecord;
        
        Assert.IsNotNull(actual);
    }


    [Test]
    public async Task Fail_PlanSprint_NoFeatures_ShouldReturnNull()
    {
        var manager = GetManager<IManageSprint>(factory.Scope);
        var plan = new PlanSprintRecord(TestData.Sprint_Success_PlanSprint_ShouldReturnNotNull, new List<string>());

        var result = await manager.PlanSprint(plan);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
  
    
    [Test]
    public async Task Fail_PlanSprint_NoSprintInfo_ShouldReturnNull()
    {
        var manager = GetManager<IManageSprint>(factory.Scope);
        var features = TestData.Features_Success_PlanSprint_ShouldReturnNotNull.Select(f => f.GUID).ToList();
        var plan = new PlanSprintRecord(TestData.Sprint_Fail_PlanSprint_NotSprintInfo_ShouldReturnNull, features);

        
        var result = await manager.PlanSprint(plan);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }


    [Test]
    public async Task Success_GetItemsForActiveSprint_WithExistingFeaturesAndTasks_ShouldReturnListOfFeaturesWithTasks()
    {
        sprintStorageMock.Setup(m => m.GetSprintInfoByProjectGUID(It.IsRegex(".*")))
            .Returns(
                SprintFactory.Create("GUID", "", "", 1, DateTime.Now, "", "", DateTime.Now)
                );
        featureStorageMock.Setup(m => m.GetFeaturesWithTasksBySprintGUID("GUID")).ReturnsAsync(
            () =>
            {
                var feature = FeatureFactory.Create("", "", "", ExtendedStatus.Refined, 2, "", "", DateTime.Now);
                var task = DataAccess.Models.Factories.TaskFactory.Create(0, "", SimpleStatus.Doing, "", "", "",
                    DateTime.Now);

                feature.Tasks = new List<DataAccess.Models.Projects.Task>()
                {
                    (DataAccess.Models.Projects.Task)task.Clone(), (DataAccess.Models.Projects.Task)task.Clone()
                };

                return new List<Feature>()
                {
                    feature
                };
            });
          
        var manager = new SprintManager(sprintStorageMock.Object, featureStorageMock.Object, null, null);

        var actualResult = await manager.GetActiveItemsForSprint("GUID");

        new ResultUnWrapper(NullLogger<ResultUnWrapper>.Instance).Unwrap(actualResult, out List<FeatureRecord> actual);
        
        Assert.That(actual.Count, Is.EqualTo(1));
        Assert.That(actual.First().Tasks.Count, Is.EqualTo(2));
    }
    
    private T GetManager<T>(IServiceScope scope)
    {
       
        var manager = scope.ServiceProvider.GetRequiredService<T>();
        return manager; 
    }
}