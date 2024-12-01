using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Utils;
using BacklogModule.Utils.Exceptions;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests;
using Task = System.Threading.Tasks.Task;


namespace BacklogModule.Tests.Management;

[TestFixture]
public class ProjectManagementTests
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
    public async Task Success_GetTeamProjects_ShouldGetNotEmptyValue()
    {
        var manager = GetManager<IManageProject>(factory.Scope);
        var team = factory.DbContext.Teams.FirstOrDefault();
    
        var result = await manager.GetAllProjectsForTeam(team.GUID);
        var actual = result.Data;
        
        Assert.IsNotEmpty(actual);
    }
    
    [Test]
    [Order(2)]
    public async Task Fail_GetTeamProjects_NonExisting_ShouldReturnEmptyList()
    {
        var manager = GetManager<IManageProject>(factory.Scope);
        
    
        var result = await manager.GetAllProjectsForTeam("");
        var actual = result.Data;
        
        Assert.IsEmpty(actual);
    }
    
    
    [Test]
    [Order(3)]
    public async Task Success_GetProjectByGuid_ShouldGetProject()
    {
        var manager = GetManager<IManageProject>(factory.Scope);
        var project = factory.DbContext.Projects.FirstOrDefault();
    
        var result = await manager.GetProjectByGuid(project.GUID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(4)]
    public async Task Fail_GetProjectByGuid_NonExisting_ShouldThrowError()
    {
        var manager = GetManager<IManageProject>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.GetProjectByGuid("");
        });
    }

    [Test]
    [Order(5)]
    public async Task Success_AddProject_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageProject>(factory.Scope);
        var project = GetProject(TestData.Project.Clone() as Project);
        
        var result = await manager.AddProject(project);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(6)]
    public async Task Success_AddEmptyProject_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageProject>(factory.Scope);
        var project = GetProject(new Project());
        
        var result = await manager.AddProject(project);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(7)]
    public async Task Fail_AddNullProject_ShouldThrowError()
    {
        var manager = GetManager<IManageProject>(factory.Scope);

        Assert.ThrowsAsync<BacklogException>(async () =>
        {
            await manager.AddProject(null);
        });
    }
    
    [Test]
    [Order(8)]
    public async Task Success_UpdateProject_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageProject>(factory.Scope);
        var project = factory.DbContext.Projects.FirstOrDefault();
        project.Name = "ABC";
        
        var result = await manager.UpdateProject(project);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(9)]
    public async Task Fail_UpdateNotExistingProject_ShouldThrowError()
    {
        var manager = GetManager<IManageProject>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.UpdateProject(new Project());
        });
    }
    
    [Test]
    [Order(10)]
    public async Task Success_DeleteProject_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageProject>(factory.Scope);
        var project = factory.DbContext.Projects.FirstOrDefault();
        
        var result = await manager.DeleteProject(project.GUID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }

    [Test]
    [Order(11)]
    public async Task Fail_DeleteProject_NotExisting_ShouldReturnNull()
    {
        var manager = GetManager<IManageProject>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.DeleteProject("");
        });
    }
    
    private Project GetProject(Project project)
    {
        var team = TestData.Team;
        project.GUID = Guid.NewGuid().ToString();
        project.CreatedBy = "ABC";
        project.Name = "ABC2";
        project.LastUpdatedBy = "ABC";
        project.TeamGUID = team.GUID;
        return project;
    }
    
    private T GetManager<T>(IServiceScope scope)
    {
       
        var manager = scope.ServiceProvider.GetRequiredService<T>();
        return manager; 
    }
}