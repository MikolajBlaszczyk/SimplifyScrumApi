using System.Security.Claims;
using DataAccess.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrum.DI;

namespace SimplfiyTestFramework.IntegrationTestsNewApproach.Hubs;

//Authorize all requests
public class GenericWebApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<SimplifyAppDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);
            
            services.AddDbContext<SimplifyAppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDB");
            });
            
            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            
            services.ConfigureDependencyInjection();
        });
    }
}

public class FakePolicyEvaluator : IPolicyEvaluator
{
    public virtual async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var principal = new ClaimsPrincipal();

        principal.AddIdentity(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Role, "FakeUser"),
            new Claim(ClaimTypes.NameIdentifier, "FakeName")
        }, "FakeScheme"));

        return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal,
            new AuthenticationProperties(), "FakeScheme")));
    }

    public virtual async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
        AuthenticateResult authenticationResult, HttpContext context, object resource)
    {
        return await Task.FromResult(PolicyAuthorizationResult.Success());
    }
}
