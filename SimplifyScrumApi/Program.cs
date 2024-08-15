using DataAccess.Context;
using DataAccess.Model;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimplifyScrum.ApiUtils;
using UserModule;
using UserModule.Abstraction;
using UserModule.Security;
using UserModule.Security.Models.Converters;
using UserModule.Security.Validation;


var builder = WebApplication.CreateBuilder(args);

var simplifyDatabaseCs = builder.Configuration.GetConnectionString("SimplifyDatabase");
builder.Services.AddDbContext<SimplifyAppDbContext>(options =>
{
    options.UseSqlServer(simplifyDatabaseCs);
});
builder.Services.AddDefaultIdentity<Teammate>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<SimplifyAppDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;
    
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._*";
#if DEBUG
    options.User.RequireUniqueEmail = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
#else
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
#endif
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});


#if  DEBUG

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

#endif

builder.Services.AddScoped<IManageSecurity, UserSecurityManager>();
builder.Services.AddScoped<LoginProcessor, LoginProcessor>();
builder.Services.AddScoped<UserValidator, UserValidator>();
builder.Services.AddScoped<AspIdentityDirector, AspIdentityDirector>();
builder.Services.AddScoped<LogoutProcessor, LogoutProcessor>();
builder.Services.AddScoped<UserAccountProcessor, UserAccountProcessor>();
builder.Services.AddScoped<UserModelConverter, UserModelConverter>();
builder.Services.AddScoped<ModuleResultInterpreter, ModuleResultInterpreter>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();