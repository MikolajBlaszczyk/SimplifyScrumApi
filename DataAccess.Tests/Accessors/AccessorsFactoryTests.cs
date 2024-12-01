using DataAccess.Accessors;
using DataAccess.Tests.TestUtils;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests;

namespace DataAccess.Tests.Accessors;

[TestFixture]
public class AccessorsFactoryTests
{
    private static WebApiFactory factory;
    
    [SetUp]
    public void Setup()
    {
        factory = new WebApiFactory();
    }

    [TearDown]
    public void TearDown()
    {
        factory.Dispose();
    }
    
    
    [Test]
    [Order(1)]
    public void Create_AddsValueToDatabase()
    {
        Add(DataAccessTestUtils.Meeting);
        Add(DataAccessTestUtils.Sprint);
        Add(DataAccessTestUtils.SprintNote);
        Add(DataAccessTestUtils.Feature);
        Add(DataAccessTestUtils.Project);
        Add(DataAccessTestUtils.Task);
        Add(DataAccessTestUtils.Team);
    }
    
    public void Add<T>(T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = accessor.Add(model);
            var count = accessorFactory.DbContext.Meetings.Count();
            
            Assert.IsNotNull(actual, $"ADD: Value for {model.GetType()} is null");
            Assert.IsTrue(count == 1, $"ADD: We added one entity for {model.GetType()} but the count is {count}");
        }
    }

    [Test]
    [Order(2)]
    public void Update_UpdatesValueInDatabase()
    {
        var meeting =  DataAccessTestUtils.Meeting;
        meeting.Name = "ABC";
        Update(meeting);
        var sprint = DataAccessTestUtils.Sprint;
        sprint.Name = "ABC";
        Update(sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        sprintNote.TeammateGUID = "ABC";
        Update(sprint);
        var feature = DataAccessTestUtils.Feature;
        feature.Name = "ABC";
        Update(feature);
        var project = DataAccessTestUtils.Project;
        project.Name = "ABC";
        Update(project);
        var task = DataAccessTestUtils.Task;
        task.Name = "ABC";
        Update(task);
        var team = DataAccessTestUtils.Team;
        team.Name = "ABC";
        Update(team);
    }
    
    public void Update<T>(T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = accessor.Update(model);
            
            Assert.IsNotNull(actual, $"UPDATE: Value for {model.GetType()} is null");
            Assert.IsTrue(actual == model, $"UPDATE: Values for {model.GetType()} are not equal");
        }
    }

    [Test]
    [Order(3)]
    public void GetByPks_ShouldReturnNotEmptyValues()
    {
        var meeting = DataAccessTestUtils.Meeting;
        GetByPK(meeting.GUID, meeting);
        var sprint = DataAccessTestUtils.Sprint;
        GetByPK(sprint.GUID, sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        GetByPK(sprintNote.ID, sprintNote);
        var feature = DataAccessTestUtils.Feature;
        GetByPK(feature.GUID, feature);
        var project = DataAccessTestUtils.Project; 
        GetByPK(project.GUID, project);
        var task = DataAccessTestUtils.Task;
        GetByPK(task.ID, task);
        var team = DataAccessTestUtils.Team;
        GetByPK(team.GUID, team);
    }
    
    public void GetByPK<T>(object pk, T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = accessor.GetByPK(pk);
            
            Assert.IsNotNull(actual, $"GET BY PK: Value for {model.GetType()} is null");
        }
    }
    
    [Test]
    [Order(4)]
    public void GetAllByPks_ShouldReturnNotEmptyValues()
    {
        var meeting = DataAccessTestUtils.Meeting;
        GetAllByPK(meeting.GUID, meeting);
        var sprint = DataAccessTestUtils.Sprint;
        GetAllByPK(sprint.GUID, sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        GetAllByPK(sprintNote.ID, sprintNote);
        var feature = DataAccessTestUtils.Feature;
        GetAllByPK(feature.GUID, feature);
        var project = DataAccessTestUtils.Project; 
        GetAllByPK(project.GUID, project);
        var task = DataAccessTestUtils.Task;
        GetAllByPK(task.ID, task);
        var team = DataAccessTestUtils.Team;
        GetAllByPK(team.GUID, team);
    }
    
    public void GetAllByPK<T>(object pk, T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = accessor.GetAllByPKs(new List<object>() {pk});
            
            Assert.IsTrue(actual.Count != 0, $"GET ALL BY PK: Values for {model.GetType()} are empty");
            Assert.IsTrue(actual.Count == 1, $"GET ALL BY PK: Ther should be only one value for {model.GetType()}");
        }
    }
    
    
    [Test]
    [Order(5)]
    public void GetAll_ShouldReturnNotEmptyValues()
    {
        GetAll(DataAccessTestUtils.Meeting);
        GetAll(DataAccessTestUtils.Sprint);
        GetAll(DataAccessTestUtils.SprintNote);
        GetAll(DataAccessTestUtils.Feature);
        GetAll(DataAccessTestUtils.Project);
        GetAll(DataAccessTestUtils.Task);
        GetAll(DataAccessTestUtils.Team);
    }
    
    public void GetAll<T>(T Model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = accessor.GetAll();
            
            Assert.IsTrue(actual?.Count == 1, $"GET ALL: There should be only 1 element  {Model.GetType()}");
        }
    }
    
    [Test]
    [Order(6)]
    public void Delete_ShouldReturnNotEmptyValues()
    {
         
        var meeting = DataAccessTestUtils.Meeting;
        Delete(meeting.GUID, meeting);
        var sprint = DataAccessTestUtils.Sprint;
        Delete(sprint.GUID, sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        Delete(sprintNote.ID, sprintNote);
        var feature = DataAccessTestUtils.Feature;
        Delete(feature.GUID, feature);
        var project = DataAccessTestUtils.Project; 
        Delete(project.GUID, project);
        var task = DataAccessTestUtils.Task;
        Delete(task.ID, task);
        var team = DataAccessTestUtils.Team;
        Delete(team.GUID, team);
    }
    
    public void Delete<T>(object pk, T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = accessor.Delete(model);
            
            Assert.IsNotNull(actual, $"DELETE: There should not be any element for {model.GetType()}");
        }
    }
}