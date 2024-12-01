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
public class FeatureManagementTests
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
    public async Task Success_GetProjectFeatures_ShouldGetNotEmptyValue()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);
        var project = factory.DbContext.Projects.FirstOrDefault();
    
        var result = await manager.GetAllFeaturesByProjectGUID(project.GUID);
        var actual = result.Data;
        
        Assert.IsNotEmpty(actual);
    }
    
    [Test]
    [Order(2)]
    public async Task Fail_GetProjectFeatures_NonExisting_ShouldReturnEmptyList()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);
        
        var result = await manager.GetAllFeaturesByProjectGUID("");
        var actual = result.Data;
        
        Assert.IsEmpty(actual);
    }
    
    
    [Test]
    [Order(3)]
    public async Task Success_GetFeatureByGuid_ShouldGetProject()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);
        var feature = factory.DbContext.Features.FirstOrDefault();
    
        var result = await manager.GetFeatureByGuid(feature.GUID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(4)]
    public async Task Fail_GetFeatureByGuid_NonExisting_ShouldThrowError()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.GetFeatureByGuid("");
        });
    }

    [Test]
    [Order(5)]
    public async Task Success_AddFeature_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);
        var feature = GetFeature(TestData.Feature.Clone() as Feature);
        
        var result = await manager.AddFeature(feature);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(6)]
    public async Task Success_AddEmptyFeature_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);
        var feature = GetFeature(new Feature());
        
        var result = await manager.AddFeature(feature);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(7)]
    public async Task Fail_AddNullFeature_ShouldThrowError()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);

        Assert.ThrowsAsync<BacklogException>(async () =>
        {
            await manager.AddFeature(null);
        });
    }
    
    [Test]
    [Order(8)]
    public async Task Success_UpdateFeature_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);
        var feature = factory.DbContext.Features.FirstOrDefault();
        feature.Name = "ABCD";
        
        var result = await manager.UpdateFeature(feature);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }
    
    [Test]
    [Order(9)]
    public async Task Fail_UpdateNotExistingFeature_ShouldThrowError()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.UpdateFeature(new Feature());
        });
    }
    
    [Test]
    [Order(10)]
    public async Task Success_DeleteFeature_ShouldReturnNotNull()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);
        var feature = factory.DbContext.Features.FirstOrDefault();
        
        var result = await manager.DeleteFeature(feature.GUID);
        var actual = result.Data;
        
        Assert.IsNotNull(actual);
    }

    [Test]
    [Order(11)]
    public async Task Fail_DeleteFeature_NotExisting_ShouldReturnNull()
    {
        var manager = GetManager<IManageFeature>(factory.Scope);

        Assert.ThrowsAsync<AccessorException>(async () =>
        {
            await manager.DeleteFeature("");
        });
    }
    
    private Feature GetFeature(Feature feature)
    {
        feature.GUID = Guid.NewGuid().ToString();
        feature.CreatedBy = "ABC";
        feature.Name = "ABC2";
        feature.Description = "asdvdfsfdsa";
        feature.LastUpdatedBy = "ABC";
        return feature;
    }
    
    private T GetManager<T>(IServiceScope scope)
    {
       
        var manager = scope.ServiceProvider.GetRequiredService<T>();
        return manager; 
    }
}