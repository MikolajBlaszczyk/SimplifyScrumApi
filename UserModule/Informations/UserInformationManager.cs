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
    IUserHierarchyAccessor hierarchyAccessor): IManageUserInformation
{
    public async Task<InformationResult> GetInfoByUserGuid(string guid)
    {
        try
        {
            var teammate = await userManager.FindByIdAsync(guid);
            var appUser = converter.ConvertToAppUser(teammate);

            return InformationResultFactory.Success(appUser);
        }
        catch (Exception ex)
        {
            return InformationResultFactory.Failure(ex);
        }
    }

    public async Task<HierarchyResult> GetUsersProject(string guid)
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

    public async Task<InformationResult> GetAllUsers()
    {
        try
        {
           var allUsers =  userManager.Users.AsEnumerable().Select(converter.ConvertToAppUser).ToList();
           return InformationResultFactory.Success(allUsers);
        }
        catch (Exception e)
        {
            return InformationResultFactory.Failure(e);
        }
    }
}