using System.Net;
using Microsoft.Extensions.DependencyInjection;
using UserModule;
using UserModule.Records;

namespace SimplifyScrumApi.Tests;

[TestFixture]
public class SprintControllerTests
{
    private WebSimpleApiFactory factory;
    private const string ApiUrl = "api/v1/scrum/sprint";
    [SetUp]
    public async Task Setup()
    {
        factory = new WebSimpleApiFactory();

        using (var scope = factory.Services.CreateScope())
        {
            var processor = scope.ServiceProvider.GetService<UserAccountProcessor>();

            var user = new SimpleUserModel("admin", "Password123!", "example@abc.com", "admin");

            await processor.SignInUserAsync(user);
        }
    }
    
    [Test]
    public async Task GetGoalOfTheSprint_ShouldReturnStatusOk()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync(ApiUrl + "/");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [TearDown]
    public void Teardown()
    {
        factory.Dispose();
    }
}