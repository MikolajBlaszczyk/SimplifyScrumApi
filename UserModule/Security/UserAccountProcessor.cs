using DataAccess.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Models.Converters;
using UserModule.Security.Validation;

namespace UserModule;

public class UserAccountProcessor(
    UserValidator validator,
    UserModelConverter converter,
    AspIdentityDirector identityDirector)
{

    public async Task<SecurityResult> SignInUserAsync(SimpleUserModel userModel)
    {
        try
        {
            var validation = validator.ValidateBeforeSignIn(userModel);
            if (validation.IsFailure)
                throw new Exception(validation.Message);

            var teammate = converter.ConvertToTeammate(userModel);
            await identityDirector.CreateUserAsync(teammate, userModel.Password);
        }
        catch (Exception e)
        {
            return e;
        }

        return SecurityResult.SuccessWithoutData();
    }

    public async Task<SecurityResult> DeleteCurrentUserAsync()
    {
        try
        {
            var userGuid = await identityDirector.GetLoggedUserGUIDAsync();
            await identityDirector.DeleteUserByGUIDAsync(userGuid);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return SecurityResult.SuccessWithoutData();
    }

    public async Task<SecurityResult> AddRoleToCurrentUserAsync(string role)
    {
        try
        {
            var user = await identityDirector.GetCurrentUserAsync();
            await identityDirector.AddRoleForUserAsync(user , role);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return SecurityResult.SuccessWithoutData();
    }

    public async Task<SecurityResult> AddRoleForUser(Teammate user, string role)
    {
        try
        {
            await identityDirector.AddRoleForUserAsync(user, role);
        }
        catch(Exception ex)
        {
            return ex;
        }

        return SecurityResult.SuccessWithoutData();
    }
    
    public async Task<string> GetCurrentUserIdAsync() => await identityDirector.GetLoggedUserGUIDAsync();
}