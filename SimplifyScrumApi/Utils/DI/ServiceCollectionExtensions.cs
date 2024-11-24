using BacklogModule;
using BacklogModule.Abstraction;
using BacklogModule.Preparation;
using BacklogModule.Preparation.Creation;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Tables;
using DataAccess.Accessors;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Identity;
using SchedulingModule;
using SchedulingModule.Abstraction;
using SchedulingModule.Utils;
using UserModule;
using UserModule.Abstraction;
using UserModule.Informations;
using UserModule.Security;
using UserModule.Security.Validation;
using Task = DataAccess.Models.Projects.Task;

namespace SimplifyScrum.DI;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        #region user

        services.AddScoped<IRoleManager, RoleManagerHelper>();
        services.AddScoped<IManageSecurity, UserSecurityManager>();
        services.AddScoped<LoginProcessor, LoginProcessor>();
        services.AddScoped<UserValidator, UserValidator>();
        services.AddScoped<AspIdentityDirector, AspIdentityDirector>();
        services.AddScoped<LogoutProcessor, LogoutProcessor>();
        services.AddScoped<UserAccountProcessor, UserAccountProcessor>();
        services.AddScoped<IManageUserInformation, UserInformationManager>();
        services.AddScoped<TokenProvider, TokenProvider>();
        
        #endregion

        #region backlog

        services.AddScoped<IManageSprint, BacklogManager>();
        services.AddScoped<IUserHierarchyAccessor, UserHierarchyAccessor>();
        services.AddScoped<ISprintAccessor, SprintAccessor > ();
        services.AddScoped<IManageProjectItems, BacklogManager>();
        services.AddScoped<IProjectItemsAccessor, ProjectItemAccessor>();
        services.AddTransient<IPrepareCreation<Project>, ProjectCreationPreparer>();
        services.AddTransient<IPrepareCreation<Feature>, FeatureCreationPreparer>();
        services.AddTransient<IPrepareCreation<Task>, TaskCreationPreparer>();
        services.AddTransient<IPrepareCreation<HistoryTable>, HistoryTableCreationPreparer>();
        services.AddSingleton<IEntityPreparerFactory, EntityPreparerFactory>();

        #endregion
        
        #region scheduling
        
        services.AddScoped<ISchedule, Scheduler>();
        services.AddScoped<IMeetingAccessor, MeetingAccessor>();
        services.AddScoped<MeetingGrouper, MeetingGrouper>();
        services.AddScoped<CalendarArranger, CalendarArranger>();
        services.AddScoped<ModelConverter, ModelConverter>();
        services.AddScoped<IManageMeetings, MeetingManager>();
        services.AddScoped<TeammateLinker, TeammateLinker>();

        #endregion

    }
}