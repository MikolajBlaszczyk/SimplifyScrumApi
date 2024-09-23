using DataAccess.Abstraction;
using DataAccess.Accessors;
using SchedulingModule;
using SchedulingModule.Abstraction;
using SchedulingModule.Utils;
using UserModule;
using UserModule.Abstraction;
using UserModule.Informations;
using UserModule.Security;
using UserModule.Security.Models.Converters;
using UserModule.Security.Validation;

namespace SimplifyScrum.DI;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        #region user

        services.AddScoped<IManageSecurity, UserSecurityManager>();
        services.AddScoped<LoginProcessor, LoginProcessor>();
        services.AddScoped<UserValidator, UserValidator>();
        services.AddScoped<AspIdentityDirector, AspIdentityDirector>();
        services.AddScoped<LogoutProcessor, LogoutProcessor>();
        services.AddScoped<UserAccountProcessor, UserAccountProcessor>();
        services.AddScoped<UserModelConverter, UserModelConverter>();
        services.AddScoped<IManageInformation, InformationManager>();
        services.AddScoped<TokenProvider, TokenProvider>();

        #endregion

        #region scheduling
        
        services.AddScoped<ISchedule, Scheduler>();
        services.AddScoped<IMeetingAccessor, MeetingAccessor>();
        services.AddScoped<MeetingGrouper, MeetingGrouper>();
        services.AddScoped<CalendarArranger, CalendarArranger>();
        services.AddScoped<ModelConverter, ModelConverter>();

        #endregion

    }
}