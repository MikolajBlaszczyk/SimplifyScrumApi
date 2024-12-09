using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Context;
using DataAccess.Enums;
using DataAccess.Model.User;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UserModule;
using UserModule.Abstraction;
using UserModule.Records;
using Task = System.Threading.Tasks.Task;

namespace SimplifyScrumApi.Tests.Utils;

public static class UserAuthorizationHelper
{
    static readonly Team Team = new()
    {
        GUID = Guid.NewGuid().ToString(),
        Name = "Test",
        ManagerGUID = ""
    };
    static readonly Teammate User = new()
    {
        UserName = "admin", Email = "example@abc.com", Nickname = "admin", TeamGUID = Team.GUID
    };
    static readonly Project Project = new()
    {
        CreatedBy = "",
        LastUpdatedBy = "",
        LastUpdateOn =  DateTime.Now,
        CreatedOn = DateTime.Now,
        GUID = Guid.NewGuid().ToString(),
        Name = "test",
        Description = "",
        State = StandardStatus.Done,
        TeamGUID = Team.GUID,
        IsActive = true
    };
    static readonly Sprint Sprint = new()
    {
        CreatedBy = "",
        LastUpdatedBy = "",
        LastUpdateOn = DateTime.Now,
        CreatedOn = DateTime.Now,
        GUID = Guid.NewGuid().ToString(),
        Name = "Test",
        Goal = "Test",
        Iteration = 0,
        End = DateTime.Today,
        ProjectGUID = Project.GUID
    };
    
    static readonly string Password = "Password123!";
    static UserManager<Teammate>? userManager;
    static SignInManager<Teammate>? signInManager;


    public static void PopulateData(WebApiFactory factory)
    {
        factory.DbContext.Teams.Add(Team);
        factory.DbContext.Projects.Add(Project);
        factory.DbContext.Sprints.Add(Sprint);

        factory.DbContext.SaveChanges();
    }
    
    public static async Task TryToCreateUser(IServiceScope scope)
    {
        if(userManager is null)
            userManager = scope.ServiceProvider.GetService<UserManager<Teammate>>();
        if(signInManager is null)
             signInManager = scope.ServiceProvider.GetService < SignInManager<Teammate>>();
        
        if (userManager!.Users.Any(u => u.UserName == User.UserName) == false)
        {
            await userManager.CreateAsync(User, Password);
        }
    }

    public static async Task Login()
    {
        await signInManager.PasswordSignInAsync(User.UserName, Password, false, false);
    }
    
    public static async Task Logout()
    {
        await signInManager.SignOutAsync();
    }
}

public static class WebApiFactoryExtensions
{
    static bool clientDataPopulated = false; 
    
    public static async Task<HttpClient> CreateLoggedClient(this WebApiFactory factory)
    {
        var client = factory.CreateClient();

        if (clientDataPopulated == false)
        {
            UserAuthorizationHelper.PopulateData(factory);
            clientDataPopulated = true;            
        }
        
        
        UserAuthorizationHelper.TryToCreateUser(factory.Scope);
        
        
        var loginResponse = await client.PostAsJsonAsync("api/v1/scrum/login", new SimpleUserModel("admin", "Password123!", "", Nickname:""));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await loginResponse.Content.ReadAsStringAsync());

        return client;
    }
}