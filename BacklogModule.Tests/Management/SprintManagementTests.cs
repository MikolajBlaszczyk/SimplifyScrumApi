using BacklogModule.Abstraction.BacklogItems;
using DataAccess.Utils;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests;

namespace BacklogModule.Tests.Management;

[TestFixture]
public class SprintManagementTests
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
    public async Task Success_GetProjectSprint_ShouldGetNotNullValue()
    {
        var manager = GetManager<IManageSprint>(factory.Scope);
        var project = factory.DbContext.Projects.FirstOrDefault();
    
        var result = await manager.GetSprintInfoByProjectGUID(project.GUID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    public async Task Success_GetProjectSprint_NonExisting_ShouldReturnNull()
    {
        var manager = GetManager<IManageSprint>(factory.Scope);

        
        var result = await manager.GetSprintInfoByProjectGUID("");
        var actual = result.Data;
        
        Assert.IsNull(actual);
    }
    
    private T GetManager<T>(IServiceScope scope)
    {
       
        var manager = scope.ServiceProvider.GetRequiredService<T>();
        return manager; 
    }
}