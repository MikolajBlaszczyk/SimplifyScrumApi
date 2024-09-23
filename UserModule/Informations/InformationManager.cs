using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using UserModule.Abstraction;
using UserModule.Models.Factories;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Models.Converters;

namespace UserModule.Informations;

public class InformationManager(UserManager<Teammate> userManager, UserModelConverter converter): IManageInformation
{
    public async Task<InformationResult> GetInfoByName(string name)
    {
        
        try
        {
            var teammate = await userManager.FindByNameAsync(name);
            var appUser = converter.ConvertToAppUser(teammate);

            return InformationResultFactory.Success(appUser);
        }
        catch (Exception ex)
        {
            return InformationResultFactory.Failure(ex);
        }
    }
}