using DataAccess.Abstraction;
using DataAccess.Tests.TestUtils;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests;

namespace DataAccess.Tests.Accessors;

public class ModelAccessorTests
{
    
    private static WebApiFactory factory;
    private static IServiceScope scope;
    
    [SetUp]
    public void Setup()
    {
        factory = new WebApiFactory();
        scope = factory.Services.CreateScope();
    }

    [TearDown]
    public void TearDown()
    {
        factory.Dispose();
        scope.Dispose();
    }
    

    [Test]
    public void AddModels_ShouldProperlyAddModels()
    {
        Create(DataAccessTestUtils.Meeting);
        Create(DataAccessTestUtils.Sprint);
        Create(DataAccessTestUtils.SprintNote);
        Create(DataAccessTestUtils.Feature);
        Create(DataAccessTestUtils.Project);
        Create(DataAccessTestUtils.Task);
        Create(DataAccessTestUtils.Team);
    }

    public static void Create<T>(T model) where T : class
    {
        using var scope = factory.Services.CreateScope();
        var (method, accessorsFactory) = DataAccessTestUtils.CreateAccessorFactoryMethodImpl(scope, model.GetType());
        var accessor = accessorsFactory.Create<T>();
        
        Assert.That(accessor, Is.InstanceOf<IAccessor<T>>(), $"Accessor should be type of IAccessor<{model.GetType()}> but is {accessor.GetType()}");
        
        
    }


}