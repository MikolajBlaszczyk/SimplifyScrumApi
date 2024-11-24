using System.Net;
using DataAccess.Enums;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests.Utils;
using UserModule;
using UserModule.Informations;
using UserModule.Records;

namespace SimplifyScrumApi.Tests;

[TestFixture]
public class SprintControllerTests
{
    private WebSimpleApiFactory factory;
    private const string ApiUrl = "api/v1/scrum/sprint";
    private SimpleUserModel _createdUserModel = new ("admin", "Password123!", "example@abc.com",  Nickname: "admin");
    
    [OneTimeSetUp]
    public async Task Setup()
    {
        factory = new WebSimpleApiFactory();
    }
    
    [Test]
    public async Task GetGoalOfTheSprint_ShouldReturnStatusOk()
    {
        var client = await factory.CreateLoggedClient();

        var response = await client.GetAsync(ApiUrl + "/info");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [OneTimeTearDown]
    public void Teardown()
    {
        factory.Dispose();
    }
}