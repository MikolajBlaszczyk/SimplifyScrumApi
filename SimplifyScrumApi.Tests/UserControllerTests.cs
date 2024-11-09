using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Enums;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UserModule;
using UserModule.Informations;
using UserModule.Records;

namespace SimplifyScrumApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private WebSimpleApiFactory factory;
    private SimpleUserModel _createdUserModel =new SimpleUserModel("admin5", "Password123!", "example@abc.com", SystemRole.User, "", "admin5", ScrumRole.ProjectOwner);
    
    [SetUp]
    public async Task Setup()
    {
        factory = new WebSimpleApiFactory();

        using (var scope = factory.Services.CreateScope())
        {
            var processor = scope.ServiceProvider.GetService<UserAccountProcessor>();
            await processor.SignInUserAsync(_createdUserModel);
        }
    }

    [Test]
    public async Task GetInfoFromExistingUser_ShouldCorrectlyGetUserInfo()
    {
        var client = factory.CreateClient();
        var loginResponse = await client.PostAsJsonAsync("api/v1/scrum/login", _createdUserModel);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await loginResponse.Content.ReadAsStringAsync());


        var response = await client.GetAsync("api/v1/scrum/user/info");
        var returnedUser = JsonConvert.DeserializeObject<SimpleUserModel>(await response.Content.ReadAsStringAsync()); 
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(returnedUser.Username, Is.EqualTo(_createdUserModel.Username));
        Assert.IsEmpty(returnedUser.Password);
        Assert.That(returnedUser.Email, Is.EqualTo(_createdUserModel.Email));
        Assert.That(returnedUser.Nickname, Is.EqualTo(_createdUserModel.Nickname));
        Assert.That(returnedUser.Role, Is.EqualTo(_createdUserModel.Role));
    }
        
    [TearDown]
    public void Teardown()
    {
        factory.Dispose();
    }
}