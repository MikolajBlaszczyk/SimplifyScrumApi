using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BacklogModule.Models;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Storage;
using DataAccess.Enums;
using DataAccess.Model.User;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests.Utils;
using UserModule;
using UserModule.Informations;
using UserModule.Records;
using Task = System.Threading.Tasks.Task;
using TaskFactory = DataAccess.Models.Factories.TaskFactory;

namespace SimplifyScrumApi.Tests;

[TestFixture]
public class ProjectItemControllerTests
{
    private WebApiFactory factory;
    private const string ApiUrl = "api/v1/scrum/";
    //TODO: start from here
          
    [OneTimeSetUp]
    public async Task Setup()
    {
      factory = new WebApiFactory();
      using (var scope = factory.Scope)
      {
          var featureStorage = scope.ServiceProvider.GetService<IFeatureStorage>();
          var projectStorage = scope.ServiceProvider.GetService<IProjectStorage>();
          var taskStorage = scope.ServiceProvider.GetService<ITaskStorage>();
          projectStorage.AddProject(ProjectFactory.Create("projectGUID", "", null, StandardStatus.New, "", "", DateTime.Now));
          featureStorage.AddFeature(FeatureFactory.Create("featureGUID", "", "", ExtendedStatus.New, null, null, "", DateTime.Now));
          taskStorage.AddTask(TaskFactory.Create(2, "", SimpleStatus.Done, "", "", "", DateTime.Now));
      }
    }

    #region add project
   
    [Test]
    public async Task AddProject_FullRecord_ShouldReturnStatusOk()
    {
        var guid = Guid.NewGuid().ToString();
        var project = ProjectRecord.Create(guid, "First project", "", StandardStatus.New, guid, guid, DateTime.Now);
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "project/add", project);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task AddProject_IncompleteNonRequiredRecord_ShouldReturnStatusOk()
    {
        var guid = Guid.NewGuid().ToString();
        var project = ProjectRecord.Create(guid, "First project", null, StandardStatus.New, guid, "", DateTime.Now);
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "project/add", project);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task AddProject_EmptyRecord_ShouldReturnStatusBadRequest()
    {
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "project/add", null as ProjectRecord);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task AddProject_IncompleteRequiredRecord_ShouldReturnStatusBadRequest()
    {
        var guid = Guid.NewGuid().ToString();
        var project = ProjectRecord.Create(null, null, "", StandardStatus.New, null, guid, DateTime.Now);
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "project/add", project);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    


    #endregion

    #region delete project

    [Test]
    public async Task DeleteExistingProject_ShouldReturnStatusOk()
    {
        
        var client = await factory.CreateLoggedClient();
        
        var response = await client.DeleteAsync(ApiUrl + "project/delete" + "?projectGUID=projectGUID");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task DeleteNonExistingProject_ShouldReturnStatusBadRequest()
    {
        
        var client = await factory.CreateLoggedClient();
        
        var response = await client.DeleteAsync(ApiUrl + "project/delete" + "?projectGUID=projectGUID2");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    #endregion
    
    #region add feature

    [Test]
    public async Task AddFeature_FullRecord_ShouldReturnStatusOk()
    {
        var guid = Guid.NewGuid().ToString();
        var feature = FeatureRecord.Create(guid, "first feature","" , ExtendedStatus.New, 0,guid, "me", DateTime.Now );
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "feature/add", feature);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task AddFeature_IncompleteNonRequiredRecord_ShouldReturnStatusOk()
    {
        var guid = Guid.NewGuid().ToString();
        var feature = FeatureRecord.Create(guid, "", "", ExtendedStatus.New, null,"", "", DateTime.Now );
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "feature/add", feature);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task AddFeature_EmptyRecord_ShouldReturnStatusBadRequest()
    {
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "feature/add", null as FeatureRecord);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task AddFeature_IncompleteRequiredRecord_ShouldReturnStatusBadRequest()
    {
        var guid = Guid.NewGuid().ToString();
        var feature = FeatureRecord.Create(null, null, "", ExtendedStatus.New, null, guid, null, DateTime.Now );
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "feature/add", feature);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    #endregion
  
    #region delete feature

    [Test]
    public async Task DeleteExistingFeature_ShouldReturnStatusOk()
    {
        
        var client = await factory.CreateLoggedClient();
        
        var response = await client.DeleteAsync(ApiUrl + "feature/delete" + "?featureGUID=featureGUID");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task DeleteNonExistingFeature_ShouldReturnStatusBadRequest()
    {
        var client = await factory.CreateLoggedClient();
        
        var response = await client.DeleteAsync(ApiUrl + "feature/delete" + "?featureGUID=featureGUID2");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    #endregion
    
    #region add task

    [Test]
    public async Task AddTask_FullRecord_ShouldReturnStatusOk()
    {
        var guid = Guid.NewGuid().ToString();
        var task = TaskRecord.Create(0, "first task", SimpleStatus.ToBeDone, guid, guid, guid, DateTime.Now);
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "task/add", task);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task AddTask_IncompleteNonRequiredRecord_ShouldReturnStatusOk()
    {
        var guid = Guid.NewGuid().ToString();
        var task = TaskRecord.Create(0, "first task", SimpleStatus.ToBeDone, guid, null, "", DateTime.Now);
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "task/add", task);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task AddTask_EmptyRecord_ShouldReturnStatusBadRequest()
    {
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "task/add", null as TaskRecord);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task AddTask_IncompleteRequiredRecord_ShouldReturnStatusBadRequest()
    {
        var guid = Guid.NewGuid().ToString();
        var task = TaskRecord.Create(-1,null, SimpleStatus.ToBeDone, null, guid, guid, DateTime.Now );
        var client = await factory.CreateLoggedClient();

        var response = await client.PostAsJsonAsync(ApiUrl + "task/add", task);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    #endregion
    
    #region delete task

    [Test]
    public async Task DeleteExistingTask_ShouldReturnStatusOk()
    {
        
        var client = await factory.CreateLoggedClient();
        
        var response = await client.DeleteAsync(ApiUrl + "task/delete" + "?taskID=2");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task DeleteNonExistingTask_ShouldReturnStatusBadRequest()
    {
        
        var client = await factory.CreateLoggedClient();
        
        var response = await client.DeleteAsync(ApiUrl + "task/delete" + "?taskID=-3");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    #endregion
    
   
      
    [Test]
    public async Task GetAllProjects_ShouldReturnStatusOk()
    {
        var client = await factory.CreateLoggedClient();

        const string url = ApiUrl + "projects";
        var response = await client.GetAsync(url);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task GetAllFeature_ShouldReturnStatusOk()
    { 
        var client = await factory.CreateLoggedClient();

        var response = await client.GetAsync(ApiUrl + "/projects/features" + "?projectGUID=''");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task GetAllTasks_ShouldReturnStatusOk()
    {
        var client = await factory.CreateLoggedClient();

        var response = await client.GetAsync(ApiUrl + "/features/tasks?featureGUID=''");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [OneTimeTearDown]
    public async Task Teardown()
    {
        factory.Dispose();
    }
}