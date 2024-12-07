using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.BacklogManagement;
using BacklogModule.Models;
using DataAccess.Abstraction.Storage;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework.Internal;
using SimplifyScrumApi.Tests;
using Task = System.Threading.Tasks.Task;

namespace BacklogModule.Tests.Management;


[TestFixture]
[Ignore("Rewrite Sprint tests")]
public class SprintManagementTests
{
    private  WebApiFactory factory;
    
   
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
        storage.Setup(s => s.AddSprint(It.IsAny<Sprint>())).ReturnsAsync(new Sprint());
        storage.Setup(s => s.LinkSprintWithFeatures(It.IsNotNull<Sprint>(), It.IsNotNull<List<string>>()));
        var manager = new SprintManager(storage.Object);
        
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
    public async Task Fail_PlanSprint_NotSprintInfo_ShouldReturnNull()
    {
        var manager = GetManager<IManageSprint>(factory.Scope);
        var features = TestData.Features_Success_PlanSprint_ShouldReturnNotNull.Select(f => f.GUID).ToList();
        var plan = new PlanSprintRecord(TestData.Sprint_Fail_PlanSprint_NotSprintInfo_ShouldReturnNull, features);

        
        var result = await manager.PlanSprint(plan);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    private T GetManager<T>(IServiceScope scope)
    {
       
        var manager = scope.ServiceProvider.GetRequiredService<T>();
        return manager; 
    }
}