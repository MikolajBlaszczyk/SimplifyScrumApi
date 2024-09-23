using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Enums;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UserModule;
using UserModule.Records;

namespace SimplifyScrumApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private WebSimpleApiFactory factory;
    private AppUser createdUser;
    
    [SetUp]
    public async Task Setup()
    {
        factory = new WebSimpleApiFactory();

        using (var scope = factory.Services.CreateScope())
        {
            var processor = scope.ServiceProvider.GetService<UserAccountProcessor>();

            createdUser = new AppUser("admin", "Password123!", "example@abc.com", "admin", ScrumRole.ProjectOwner);

            await processor.SignInUser(createdUser);
        }
    }

    [Test]
    public async Task GetInfoFromExistingUser_ShouldCorrectlyGetUserInfo()
    {
        var client = factory.CreateClient();
        var loginResponse = await client.PostAsJsonAsync("api/v1/scrum/login", createdUser);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await loginResponse.Content.ReadAsStringAsync());


        var response = await client.GetAsync("api/v1/scrum/user/info");
        var returnedUser = JsonConvert.DeserializeObject<AppUser>(await response.Content.ReadAsStringAsync()); 
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(returnedUser.Username, Is.EqualTo(createdUser.Username));
        Assert.IsEmpty(returnedUser.Password);
        Assert.That(returnedUser.Email, Is.EqualTo(createdUser.Email));
        Assert.That(returnedUser.Nickname, Is.EqualTo(createdUser.Nickname));
        Assert.That(returnedUser.Role, Is.EqualTo(createdUser.Role));
    }
        
    [TearDown]
    public void Teardown()
    {
        factory.Dispose();
    }
}