using System.Text;
using System.Text.Json;
using DataAccess.Context;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimplifyScrum.DI;
using SimplifyScrum.Utils.LifeCycle.Startup;


var builder = WebApplication.CreateBuilder(args);

var simplifyDatabaseCs = builder.Configuration.GetConnectionString("SimplifyDatabase");
builder.Services.AddDbContext<SimplifyAppDbContext>(options =>
{
    options.UseSqlServer(simplifyDatabaseCs);
});
builder.Services.AddDefaultIdentity<Teammate>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SimplifyAppDbContext>()
    .AddDefaultTokenProviders();

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
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

    
#endif
builder.Services.ConfigureDependencyInjection();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
builder.Services.AddSignalR();


#region MyRegion

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

await RoleSeeder.SeedAsync(app.Services);
await RoleSeeder.AddAdminRolesForSelectedUsers(app.Services, new[] { "64e1b3c0-7107-45e2-9324-7ae976543467" });

app.MapControllers();
app.Run();

public partial class Program { }