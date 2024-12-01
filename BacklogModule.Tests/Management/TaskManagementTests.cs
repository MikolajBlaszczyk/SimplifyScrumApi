using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Utils;
using BacklogModule.Utils.Exceptions;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests;
using Task = DataAccess.Models.Projects.Task;

namespace BacklogModule.Tests.Management;

[TestFixture]
public class TaskManagementTests
{
    private  WebApiFactory factory;
    
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
    public async System.Threading.Tasks.Task Success_GetFeatureTasks_ShouldGetNotEmptyValue()
    {
        var manager = GetManager<IManageTask>(factory.Scope);
        var feature = factory.DbContext.Features.FirstOrDefault();
    
        var result = await manager.GetAllTasksByFeatureGUID(feature.GUID);
        var actual = result.Data;
        
        Assert.IsNotEmpty(actual);
    }
    
    [Test]
    [Order(2)]
    public async System.Threading.Tasks.Task Fail_GetFeatureTasks_NonExisting_ShouldReturnEmptyList()
    {
        var manager = GetManager<IManageTask>(factory.Scope);
        
        var result = await manager.GetAllTasksByFeatureGUID("");
        var actual = result.Data;
        
        Assert.IsEmpty(actual);
    }
    
    
    [Test]
    [Order(3)]
    public async System.Threading.Tasks.Task Success_GetTaskById_ShouldGetProject()
    {
        var manager = GetManager<IManageTask>(factory.Scope);
        var task = factory.DbContext.Tasks.FirstOrDefault();
    
        var result = await manager.GetTaskById(task.ID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(4)]
    public async System.Threading.Tasks.Task Fail_GetTaskById_NonExisting_ShouldThrowError()
    {
        var manager = GetManager<IManageTask>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.GetTaskById(-1);
        });
    }

    [Test]
    [Order(5)]
    public async System.Threading.Tasks.Task Success_AddTask_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageTask>(factory.Scope);
        var task = GetTask(TestData.Task.Clone() as Task);
        
        var result = await manager.AddTask(task);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(6)]
    public async System.Threading.Tasks.Task Success_AddEmptyTask_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageTask>(factory.Scope);
        var task = GetTask(new Task());
        
        var result = await manager.AddTask(task);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(7)]
    public async System.Threading.Tasks.Task Fail_AddNullTask_ShouldThrowError()
    {
        var manager = GetManager<IManageTask>(factory.Scope);

        Assert.ThrowsAsync<BacklogException>(async () =>
        {
            await manager.AddTask(null);
        });
    }
    
    [Test]
    [Order(8)]
    public async System.Threading.Tasks.Task Success_UpdateTask_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageTask>(factory.Scope);
        var task = factory.DbContext.Tasks.FirstOrDefault();
        task.Name = "ABCD";
        
        var result = await manager.UpdateTask(task);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(9)]
    public async System.Threading.Tasks.Task Fail_UpdateNotExistingTask_ShouldThrowError()
    {
        var manager = GetManager<IManageTask>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.UpdateTask(new Task());
        });
    }
    
    [Test]
    [Order(10)]
    public async System.Threading.Tasks.Task Success_DeleteTask_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageTask>(factory.Scope);
        var task = factory.DbContext.Tasks.FirstOrDefault();
        
        var result = await manager.DeleteTask(task.ID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }

    [Test]
    [Order(11)]
    public async System.Threading.Tasks.Task Fail_DeleteTask_NotExisting_ShouldReturnNull()
    {
        var manager = GetManager<IManageTask>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.DeleteTask(-1);
        });
    }
    
    private Task GetTask(Task task)
    {
        var feature = TestData.Feature;
        task.FeatureGUID = feature.GUID;
        task.ID = Random.Shared.Next();
        task.CreatedBy = "ABC";
        task.Name = "ABC2";
        task.LastUpdatedBy = "ABC";
        return task;
    }
    
    private T GetManager<T>(IServiceScope scope)
    {
       
        var manager = scope.ServiceProvider.GetRequiredService<T>();
        return manager; 
    }
}