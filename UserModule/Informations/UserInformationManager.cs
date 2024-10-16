using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using UserModule.Abstraction;
using UserModule.Models.Factories;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Models.Converters;

namespace UserModule.Informations;

public class UserInformationManager(UserManager<Teammate> userManager, UserModelConverter converter): IManageUserInformation
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