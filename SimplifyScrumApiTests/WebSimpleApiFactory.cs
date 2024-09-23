using DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SimplifyScrumApi.Tests;

public class WebSimpleApiFactory : WebApplicationFactory<Program>
{
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
            
        });
    }
}