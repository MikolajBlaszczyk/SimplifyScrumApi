using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataAccess.Context;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrum.DI;
using UserModule.Records;
using Task = System.Threading.Tasks.Task;

namespace SimplifyScrumApi.Tests;

public class WebApiFactory : WebApplicationFactory<Program>
{
    private static bool _dataPopulated = false;
    private static readonly object _lock = new object();
    
    private IServiceScope? _scope;
    private SimplifyAppDbContext? _dbContext;

    public SimplifyAppDbContext DbContext => _dbContext ??= CreateDbContext();
    public IServiceScope Scope => _scope ??= Services.CreateScope();

    private SimplifyAppDbContext CreateDbContext()
    {
        return Scope.ServiceProvider.GetRequiredService<SimplifyAppDbContext>();
    }

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
                options.UseInMemoryDatabase("SimpleTestDB");
            });

            services.ConfigureDependencyInjection();
        });
    }

    public void PopulateTestData()
    {
        lock (_lock)
        {
            if (_dataPopulated) return;

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SimplifyAppDbContext>();

            context.Database.EnsureCreated();

            if (!context.Teams.Any())
            {
                var user = TestData.User;
                var team = TestData.Team;
                var project = TestData.Project;
                var feature = TestData.Feature;
                var task = TestData.Task;
                var sprint = TestData.Sprint;

                context.Users.Add(user);
                context.Teams.Add(team);

                sprint.ProjectGUID = project.GUID;
                project.TeamGUID = team.GUID;
                feature.ProjectGUID = project.GUID;
                task.FeatureGUID = feature.GUID;

                context.Sprints.Add(sprint);
                context.Projects.Add(project);
                context.Features.Add(feature);
                context.Tasks.Add(task);

                var feature2 = feature.Clone() as Feature;
                var task2 = task.Clone() as DataAccess.Models.Projects.Task;
                feature2.ProjectGUID = project.GUID;
                task2.FeatureGUID = feature2.GUID;
                task2.ID = Random.Shared.Next();

                context.Features.Add(feature2);
                context.Tasks.Add(task2);

                context.SaveChanges();
            }

            _dataPopulated = true;
        }
    }

    public override ValueTask DisposeAsync()
    {
        _scope?.Dispose();
        _dbContext?.Dispose();
        return base.DisposeAsync();
    }
}