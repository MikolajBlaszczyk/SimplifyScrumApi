using System.Linq.Expressions;
using DataAccess.Accessors;
using DataAccess.Context;
using DataAccess.Model.Meetings;
using DataAccess.Tests.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SimplifyScrumApi.Tests;

namespace DataAccess.Tests.Accessors;

[TestFixture]
public class ModelAccessorTests
{
    private static WebApiFactory factory;
    private Mock<SimplifyAppDbContext> _mockDbContext;
    private Mock<DbSet<Meeting>> _mockDbSet;
    private Mock<ILogger<ModelAccessor<Meeting>>> _mockLogger;
    private ModelAccessor<Meeting> _modelAccessor;
    
    [SetUp]
    public void Setup()
    {
        factory = new WebApiFactory();
        _mockDbSet = new Mock<DbSet<Meeting>>();
        _mockDbContext = new Mock<SimplifyAppDbContext>(new DbContextOptions<SimplifyAppDbContext>());
        _mockLogger = new Mock<ILogger<ModelAccessor<Meeting>>>();

        _mockDbContext.Setup(m => m.Set<Meeting>()).Returns(_mockDbSet.Object);

        _modelAccessor = new ModelAccessor<Meeting>(_mockDbContext.Object, _mockLogger.Object);
    }

    [TearDown]
    public void TearDown()
    {
        factory.Dispose();
    }
    
    
    [Test]
    [Order(1)]
    public async Task Create_AddsValueToDatabase()
    {
        await Add(DataAccessTestUtils.Meeting);
        await Add(DataAccessTestUtils.Sprint);
        await Add(DataAccessTestUtils.SprintNote);
        await Add(DataAccessTestUtils.Feature);
        await Add(DataAccessTestUtils.Project);
        await Add(DataAccessTestUtils.Task);
        await Add(DataAccessTestUtils.Team);
        await Add(DataAccessTestUtils.Notification);
    }
    
    public async Task Add<T>(T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = await accessor.Add(model);
            var count = accessorFactory.DbContext.Meetings.Count();
            
            Assert.IsNotNull(actual, $"ADD: Value for {model.GetType()} is null");
            Assert.IsTrue(count == 1, $"ADD: We added one entity for {model.GetType()} but the count is {count}");
        }
    }
    
    [Test]
    public async Task Add_ShouldThrowException_WhenAddFails()
    {
        
        _mockDbSet.Setup(m => m.AddAsync(It.IsAny<Meeting>(), default)).ThrowsAsync(new Exception("Add failed"));

        var model = new Meeting();

        var result = await _modelAccessor.Add(model);

        Assert.IsNull(result);
    }

    [Test]
    [Order(2)]
    public async Task Update_UpdatesValueInDatabase()
    {
        var meeting =  DataAccessTestUtils.Meeting;
        meeting.Name = "ABC";
        await Update(meeting);
        var sprint = DataAccessTestUtils.Sprint;
        sprint.Name = "ABC";
        await Update(sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        sprintNote.TeammateGUID = "ABC";
        await Update(sprint);
        var feature = DataAccessTestUtils.Feature;
        feature.Name = "ABC";
        await Update(feature);
        var project = DataAccessTestUtils.Project;
        project.Name = "ABC";
        await Update(project);
        var task = DataAccessTestUtils.Task;
        task.Name = "ABC";
        await Update(task);
        var team = DataAccessTestUtils.Team;
        team.Name = "ABC";
        await Update(team);
        var notification = DataAccessTestUtils.Notification;
        notification.NotificationSourceGUID = "ABC";
        await Update(notification);
    }
    
    [Test]
    public async Task Update_ShouldThrowException_WhenUpdateFails()
    {
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ThrowsAsync(new Exception("Update failed"));

        var model = new Meeting();

        var result = await _modelAccessor.Update(model);

        Assert.IsNull(result);
    }
    
    public async Task Update<T>(T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = await accessor.Update(model);
            
            Assert.IsNotNull(actual, $"UPDATE: Value for {model.GetType()} is null");
            Assert.IsTrue(actual == model, $"UPDATE: Values for {model.GetType()} are not equal");
        }
    }

    [Test]
    [Order(3)]
    public async Task GetByPks_ShouldReturnNotEmptyValues()
    {
        var meeting = DataAccessTestUtils.Meeting;
        await GetByPK(meeting.GUID, meeting);
        var sprint = DataAccessTestUtils.Sprint;
        await GetByPK(sprint.GUID, sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        await GetByPK(sprintNote.ID, sprintNote);
        var feature = DataAccessTestUtils.Feature;
        await GetByPK(feature.GUID, feature);
        var project = DataAccessTestUtils.Project; 
        await GetByPK(project.GUID, project);
        var task = DataAccessTestUtils.Task;
        await GetByPK(task.ID, task);
        var team = DataAccessTestUtils.Team;
        await GetByPK(team.GUID, team);
        var notification = DataAccessTestUtils.Notification; 
        await GetByPK(notification.ID, notification);
    }
    
    public async Task GetByPK<T>(object pk, T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = await accessor.GetByPK(pk);
            
            Assert.IsNotNull(actual, $"GET BY PK: Value for {model.GetType()} is null");
        }
    }
    
    [Test]
    public async Task GetByPK_ShouldThrowException_WhenGetByPKFails()
    {
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ThrowsAsync(new Exception("GetByPK failed"));

        var result = await _modelAccessor.GetByPK(It.IsAny<object>());

        Assert.IsNull(result);
    }
    
    [Test]
    [Order(4)]
    public async Task GetAllByPks_ShouldReturnNotEmptyValues()
    {
        var meeting = DataAccessTestUtils.Meeting;
        await GetAllByPK(meeting.GUID, meeting);
        var sprint = DataAccessTestUtils.Sprint;
        await GetAllByPK(sprint.GUID, sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        await GetAllByPK(sprintNote.ID, sprintNote);
        var feature = DataAccessTestUtils.Feature;
        await GetAllByPK(feature.GUID, feature);
        var project = DataAccessTestUtils.Project; 
        await GetAllByPK(project.GUID, project);
        var task = DataAccessTestUtils.Task;
        await GetAllByPK(task.ID, task);
        var team = DataAccessTestUtils.Team;
        await GetAllByPK(team.GUID, team);
        var notification = DataAccessTestUtils.Notification; 
        await GetAllByPK(notification.ID, notification);
    }
    
    
    public async Task GetAllByPK<T>(object pk, T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = await accessor.GetAllByPKs(new List<object>() {pk});
            
            Assert.IsTrue(actual.Count != 0, $"GET ALL BY PK: Values for {model.GetType()} are empty");
            Assert.IsTrue(actual.Count == 1, $"GET ALL BY PK: Ther should be only one value for {model.GetType()}");
        }
    }
    
    [Test]
    [Order(5)]
    public async Task GetAll_ShouldReturnNotEmptyValues()
    {
        await GetAll(DataAccessTestUtils.Meeting);
        await GetAll(DataAccessTestUtils.Sprint);
        await GetAll(DataAccessTestUtils.SprintNote);
        await GetAll(DataAccessTestUtils.Feature);
        await GetAll(DataAccessTestUtils.Project);
        await GetAll(DataAccessTestUtils.Task);
        await GetAll(DataAccessTestUtils.Team);
        await GetAll(DataAccessTestUtils.Notification);
    }
    
    public async Task GetAll<T>(T Model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = await accessor.GetAll();
            
            Assert.IsTrue(actual?.Count == 1, $"GET ALL: There should be only 1 element  {Model.GetType()}");
        }
    }
    
   
    
    [Test]
    [Order(6)]
    public async Task Delete_ShouldReturnNotEmptyValues()
    {
         
        var meeting = DataAccessTestUtils.Meeting;
        await Delete(meeting.GUID, meeting);
        var sprint = DataAccessTestUtils.Sprint;
        await Delete(sprint.GUID, sprint);
        var sprintNote = DataAccessTestUtils.SprintNote;
        await Delete(sprintNote.ID, sprintNote);
        var feature = DataAccessTestUtils.Feature;
        await Delete(feature.GUID, feature);
        var project = DataAccessTestUtils.Project; 
        await Delete(project.GUID, project);
        var task = DataAccessTestUtils.Task;
        await Delete(task.ID, task);
        var team = DataAccessTestUtils.Team;
        await Delete(team.GUID, team);
        var notification = DataAccessTestUtils.Notification;
        await Delete(notification.ID, notification);
    }
    
    public async Task Delete<T>(object pk, T model) where T : class
    {
        using (var scope = factory.Services.CreateScope())
        {
            var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider);
            var accessor = accessorFactory.Create<T>();

            var actual = await accessor.Delete(model);
            
            Assert.IsNotNull(actual, $"DELETE: There should not be any element for {model.GetType()}");
        }
    }
    
    [Test]
    public async Task Delete_ShouldThrowException_WhenDeleteFails()
    {
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ThrowsAsync(new Exception("Delete failed"));

        var model = new Meeting();

        var result = await _modelAccessor.Delete(model);

        Assert.IsNull(result);
    }
}