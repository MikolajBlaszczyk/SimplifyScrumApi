using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Enums;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimplifyScrumApi.Tests.Utils;
using UserModule;
using UserModule.Informations;
using UserModule.Records;

namespace SimplifyScrumApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private WebSimpleApiFactory factory;
    private SimpleUserModel _createdUserModel =new ("admin77", "Password123!", "example@abc.com", "", "admin77", ScrumRole.ProjectOwner);
    
    [OneTimeSetUp]
    public async Task Setup()
    {
        factory = new WebSimpleApiFactory();
    }

    [Test]
    public async Task GetInfoFromExistingUser_ShouldCorrectlyGetUserInfo()
    {
        var client = factory.CreateClient();

        var signInResponse = await client.PostAsJsonAsync("api/v1/scrum/signin", _createdUserModel);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await signInResponse.Content.ReadAsStringAsync());
        var response = await client.GetAsync("api/v1/scrum/user/info");
        var returnedUser = JsonConvert.DeserializeObject<SimpleUserModel>(await response.Content.ReadAsStringAsync()); 
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(returnedUser.Username, Is.EqualTo(_createdUserModel.Username));
        Assert.IsEmpty(returnedUser.Password);
        Assert.That(returnedUser.Email, Is.EqualTo(_createdUserModel.Email));
        Assert.That(returnedUser.Nickname, Is.EqualTo(_createdUserModel.Nickname));
        Assert.That(returnedUser.Role, Is.EqualTo(_createdUserModel.Role));
    }
        
    [OneTimeTearDown]
    public async Task Teardown()
    {
        await factory.OnDispose();
        factory.Dispose();
    }
}