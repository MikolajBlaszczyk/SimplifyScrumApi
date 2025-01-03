using BacklogModule;
using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.BacklogManagement;
using BacklogModule.Preparation;
using BacklogModule.Preparation.Creation;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Abstraction.Tables;
using DataAccess.Accessors;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using DataAccess.Models.Notifications;
using DataAccess.Models.Projects;
using DataAccess.Storage;
using Microsoft.AspNetCore.Identity;
using OpenConnectionManagement.Abstraction;
using OpenConnectionManagement.Notifications;
using SchedulingModule;
using SchedulingModule.Abstraction;
using SchedulingModule.Scheduling;
using SchedulingModule.Utils;
using SimplifyFramework.Cache;
using SimplifyFramework.Cache.Implementation.KeyValue;
using SimplifyScrum.Utils;
using SimplifyScrum.Utils.Requests;
using UserModule;
using UserModule.Abstraction;
using UserModule.Informations;
using UserModule.Security;
using UserModule.Security.Validation;
using Task = DataAccess.Models.Projects.Task;

namespace SimplifyScrum.DI;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration? configuration = null)
    {
        bool useTestingNotificationSender = configuration?.GetValue<bool>("NotificationSettings:UseTestingNotificationSender") ?? false;
        
        #region Http Requests

        services.AddScoped<ResultUnWrapper>();
        
        #if DEBUG
        if (useTestingNotificationSender)
        {
            services.AddScoped<INotificationSender, TestingNotificationSender>();
        }
        else
        {
            services.AddScoped<INotificationSender, MeetingNotificationSender>();
        }
        #else
        services.AddScoped<INotificationSender, MeetingNotificationSender>();
        #endif
   

        #endregion
        
        #region Simplfiy Framework

        services.AddTransient<ICacheKeyValuePairs, InMemoryKeyValueCache>();
        
        #endregion
        
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

        services.AddScoped<IManageFeature, FeatureManager>();
        services.AddScoped<IManageSprint, SprintManager>();
        services.AddScoped<IManageProject, ProjectManager>();
        services.AddScoped<IManageTask, TaskManager>();
        services.AddScoped<IManageBacklog, BacklogManager>();
        
        services.AddTransient<IPrepareCreation<Project>, ProjectCreationPreparer>();
        services.AddTransient<IPrepareCreation<Feature>, FeatureCreationPreparer>();
        services.AddTransient<IPrepareCreation<Sprint>, SprintCreationPreparer>();
        services.AddTransient<IPrepareCreation<Task>, TaskCreationPreparer>();
        services.AddTransient<IPrepareCreation<HistoryTable>, HistoryTableCreationPreparer>();
        services.AddSingleton<IEntityPreparerFactory, EntityPreparerFactory>();

        #endregion
        
        #region scheduling
        
        services.AddScoped<ISchedule, Scheduler>();
        services.AddScoped<IMeetingStorage, MeetingStorage>();
        services.AddScoped<MeetingGrouper, MeetingGrouper>();
        services.AddScoped<CalendarArranger, CalendarArranger>();
        services.AddScoped<ModelConverter, ModelConverter>();
        services.AddScoped<IScheduleMeetings, MeetingSchedulerManager>();
        services.AddScoped<UserLinker, UserLinker>();
        
        #endregion

        #region DataAccess
        
      
        services.AddScoped<IMeetingStorage, MeetingStorage>();
        services.AddScoped<IUserHierarchyStorage, UserHierarchyStorage>();
        services.AddScoped<ISprintStorage, SprintStorage > ();
        services.AddScoped<IFeatureStorage, FeatureStorage>();
        services.AddScoped<ITaskStorage, TaskStorage>();
        services.AddScoped<IProjectStorage, ProjectStorage>();
        services.AddScoped<ISprintNoteStorage, SprintNoteStorage>();
        services.AddScoped<INotificationStorage, NotificationStorage>();
        
        services.AddScoped<ICreateAccessors, ModelAccessorFactory>();
        services.AddScoped(typeof(IAccessor<Meeting>), typeof(ModelAccessor<Meeting>));
        services.AddScoped(typeof(IAccessor<Sprint>), typeof(ModelAccessor<Sprint>));
        services.AddScoped(typeof(IAccessor<SprintNote>), typeof(ModelAccessor<SprintNote>));
        services.AddScoped(typeof(IAccessor<Feature>), typeof(ModelAccessor<Feature>));
        services.AddScoped(typeof(IAccessor<Task>), typeof(ModelAccessor<Task>));
        services.AddScoped(typeof(IAccessor<Project>), typeof(ModelAccessor<Project>));
        services.AddScoped(typeof(IAccessor<Team>), typeof(ModelAccessor<Team>));
        services.AddScoped(typeof(IAccessor<Notification>), typeof(ModelAccessor<Notification>));

        #endregion
        
    }
}