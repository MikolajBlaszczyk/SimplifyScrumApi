using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Enums;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UserModule;
using UserModule.Informations;
using UserModule.Records;

namespace SimplifyScrumApi.Tests;

[TestFixture]
public class LoginControllerTests
{
    private WebSimpleApiFactory factory;
    
    [SetUp]
    public async Task Setup()
    {
        factory = new WebSimpleApiFactory();

        using (var scope = factory.Services.CreateScope())
        {
            var processor = scope.ServiceProvider.GetService<UserAccountProcessor>();

            var user = new SimpleUserModel("admin", "Password123!", "example@abc.com",  Nickname: "admin");

            await processor.SignInUserAsync(user);
        }
    }
    

    
    [Test]
    public async Task LoginWithExistingAccount_ShouldAuthorizeUserAndReturnToken()
    {
        using var client = factory.CreateClient();
        
        var response = await client.PostAsJsonAsync("api/v1/scrum/login", new SimpleUserModel("admin", "Password123!", ""));
        var body = await response.Content.ReadAsStringAsync();
        var token =  new JwtSecurityTokenHandler().ReadToken(body); 
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(token);
    }

    [Test]
    public async Task LoginWithNonExistingAccount_ShouldReturnInternalServerError()
    {
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("api/v1/scrum/login", new SimpleUserModel("admin", "password", "", ""));
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task LogoutWithoutLoggedUser_ShouldReturnUnauthorized()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("api/v1/scrum/logout");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
    
    [Test]
    public async Task LogoutAfterLogin_ShouldReturnOk()
    {
        using var client = factory.CreateClient();
        var loginResponse = await client.PostAsJsonAsync("api/v1/scrum/login", new SimpleUserModel("admin", "Password123!", ""));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await loginResponse.Content.ReadAsStringAsync());
        var response = await client.GetAsync("api/v1/scrum/logout");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task SignInNewUser_ShouldCreateAppropriateUser()
    {
        using (var scope = factory.Services.CreateScope())
        {
            using var client = factory.CreateClient();
            var appUser = new SimpleUserModel("Admin2", "Password1234!", "example2@abc.com", SystemRole.User, "", "Admin2", ScrumRole.DevelopmentTeam);
            var userManager = scope.ServiceProvider.GetService<UserManager<Teammate>>();
            var response = await client.PostAsJsonAsync("api/v1/scrum/signin", appUser);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var user = await userManager.FindByNameAsync(appUser.Username);
            Assert.IsNotNull(user);
        }
    }
    
    [Test]
    public async Task SignInNewUserWithIncorrectPassword_ShouldReturnInternalServerError()
    {
        using (var scope = factory.Services.CreateScope())
        {
            using var client = factory.CreateClient();
            var appUser = new SimpleUserModel("Admin3", "password34!", "example3@abc.com", SystemRole.User, "", "admin3", ScrumRole.DevelopmentTeam);
            var userManager = scope.ServiceProvider.GetService<UserManager<Teammate>>();
            
            var response = await client.PostAsJsonAsync("api/v1/scrum/signin", appUser);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

            var user = await userManager.FindByNameAsync(appUser.Username);
            Assert.IsNull(user);
        }
    }

    [Test]
    public async Task DeleteExistingUser_ShouldCorrectlyDeleteUser()
    {
        using (var scope = factory.Services.CreateScope())
        {
            using var client = factory.CreateClient();
            var appUser = new SimpleUserModel("Admin4", "Password1234!", "example4@abc.com", SystemRole.User,"", "Admin4", ScrumRole.DevelopmentTeam);
            var userManager = scope.ServiceProvider.GetService<UserManager<Teammate>>();
            var signInResponse = await client.PostAsJsonAsync("api/v1/scrum/signin", appUser);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await signInResponse.Content.ReadAsStringAsync());
            
            var response = await client.DeleteAsync("api/v1/scrum/delete");
            var user = await userManager.FindByNameAsync(appUser.Username);
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNull(user);
        }
    }

    [Test]
    public async Task DeleteNonExistingUser_ShouldReturnUnauthorized()
    {
        using var client = factory.CreateClient();
        
        var response = await client.DeleteAsync("api/v1/scrum/delete");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
    
    [TearDown]
    public void Teardown()
    {
        factory.Dispose();
    }
}