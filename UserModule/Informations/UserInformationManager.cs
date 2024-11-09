using DataAccess.Abstraction;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using UserModule.Abstraction;
using UserModule.Exceptions;
using UserModule.Models;
using UserModule.Models.Factories;
using UserModule.Security.Models;
using UserModule.Security.Models.Converters;

namespace UserModule.Informations;

public class UserInformationManager(
    UserManager<Teammate> userManager,
    UserModelConverter converter,
    IRoleManager roleManager,
    IUserHierarchyAccessor hierarchyAccessor): IManageUserInformation
{
    public async Task<UserInfoResult> GetInfoByUserGUIDAsync(string guid)
    {
        try
        {
            var teammate = await userManager.FindByIdAsync(guid);
            var role = await roleManager.GetHighestSystemRoleAsync(teammate);
            
            var appUser = converter.ConvertToAppUser(teammate, role);

            return appUser;
        }
        catch (Exception ex)
        {
            return (UserInfoResult)ex;
        }
    }

    public async Task<HierarchyResult> GetUsersProjectAsync(string guid)
    {
        try
        {
            var user = await userManager.FindByIdAsync(guid);
            if (user is null)
                throw new Exception();

            var project = hierarchyAccessor.GetProjectByTeam(user.TeamGUID);

            return HierarchyResultFactory.Success(project);

        }
        catch (Exception e)
        {
            return HierarchyResultFactory.Failure(e);
        }
    }

    public async Task<UsersInfoResult> GetAllUsersAsync()
    {
        try
        {
           var allUsers =  userManager
               .Users
               .AsEnumerable()
               .Select( async user => converter.ConvertToAppUser(user, await roleManager.GetHighestSystemRoleAsync(user)))
               .Select(task => task.Result)
               .ToList();
           
           return allUsers;
        }
        catch (Exception e)
        {
            return (UsersInfoResult)e;
        }
    }
}