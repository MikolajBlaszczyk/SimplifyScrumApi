using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Context;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrum.DI;
using SimplifyScrumApi.Tests.Utils;
using UserModule;
using UserModule.Records;

namespace SimplifyScrumApi.Tests;

public class WebSimpleApiFactory : WebApplicationFactory<Program>
{
    
    private static SimpleUserModel user =
        new SimpleUserModel("admin77", "Password123!", "example@abc.com", Nickname: "admin77");

    private HttpClient? _client;
    
    public async Task<HttpClient> CreateLoggedClient()
    {
        if (_client is not null)
            return _client;
        
        _client = CreateClient();
     
        var loginResponse = await _client.PostAsJsonAsync("api/v1/scrum/signin", new SimpleUserModel("admin", "Password123!", "example@abc.com", Nickname:"admin"));
        if(loginResponse.StatusCode == HttpStatusCode.InternalServerError)
            loginResponse = await _client.PostAsJsonAsync("api/v1/scrum/login", new SimpleUserModel("admin", "Password123!", "example@abc.com", Nickname:"admin"));
            
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await loginResponse.Content.ReadAsStringAsync());
        return _client;
    }

    public async Task OnDispose()
    {
        if(_client is not null)
            await _client.DeleteAsync("api/v1/scrum/delete");
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<SimplifyAppDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);
            
            services.AddDbContext<SimplifyAppDbContext>(options =>
            {
                options.UseInMemoryDatabase("SimpleTestDB");
            });

            services.ConfigureDependencyInjection();
        });
    }
}