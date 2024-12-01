using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Context;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UserModule;
using UserModule.Abstraction;
using UserModule.Records;

namespace SimplifyScrumApi.Tests.Utils;

public static class UserAuthorizationHelper
{
    static readonly Teammate User = new Teammate() { UserName = "admin", Email = "example@abc.com", Nickname = "admin"};
    static readonly string Password = "Password123!";
    static UserManager<Teammate>? userManager;
    static SignInManager<Teammate>? signInManager; 
    
    
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
    public static async Task<HttpClient> CreateLoggedClient(this WebApiFactory factory)
    {
        var client = factory.CreateClient();

        UserAuthorizationHelper.TryToCreateUser(factory.Scope);
        
        var loginResponse = await client.PostAsJsonAsync("api/v1/scrum/login", new SimpleUserModel("admin", "Password123!", "", Nickname:""));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await loginResponse.Content.ReadAsStringAsync());

        return client;
    }
}