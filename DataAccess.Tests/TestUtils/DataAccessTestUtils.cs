using System.Reflection;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Accessors;
using DataAccess.Enums;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using Microsoft.Extensions.DependencyInjection;
using SimplifyScrumApi.Tests;
using Task = DataAccess.Models.Projects.Task;
using TaskFactory = DataAccess.Models.Factories.TaskFactory;

namespace DataAccess.Tests.TestUtils;

public static class DataAccessTestUtils
{
    public static Meeting Meeting = TestData.Meeting;

    public static Sprint Sprint = TestData.Sprint;

    public static SprintNote SprintNote = TestData.SprintNote;

    public static Feature Feature = TestData.Feature;

    public static Project Project = TestData.Project;

    public static Task Task = TestData.Task;

    public static Team Team = TestData.Team;
    
    public static (MethodInfo, ICreateAccessors) CreateAccessorFactoryMethodImpl(IServiceScope scope, Type model)
    {
        var accessorFactory = new ModelAccessorFactory(scope.ServiceProvider); // new ModelAccessorFactory(factory.Services);
        var createMethod = typeof(ICreateAccessors).GetMethod("Create");
        var genericMethod = createMethod.MakeGenericMethod(model);

        return (genericMethod, accessorFactory);
    }
}