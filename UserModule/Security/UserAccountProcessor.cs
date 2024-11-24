using DataAccess.Model.User;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Validation;

namespace UserModule;

public class UserAccountProcessor(
    UserValidator validator,
    AspIdentityDirector identityDirector)
{

    
    public async Task<SecurityResult> SignInUserAsync(SimpleUserModel userModel)
    {
        try
        {
            var validation = validator.ValidateBeforeSignIn(userModel);
            if (validation.IsFailure)
                throw new Exception(validation.Message);

            Teammate teammate = userModel;
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

    public async Task<SecurityResult> AddRoleForUser(SimpleUserModel user, string role)
    {
        try
        {
            Teammate teammate = user;
            await identityDirector.AddRoleForUserAsync(teammate, role);
        }
        catch(Exception ex)
        {
            return ex;
        }

        return SecurityResult.SuccessWithoutData();
    }
    
    public async Task<SecurityResult> GetUserRoles(string userGUID)
    {
        try
        {
            var user = await identityDirector.GetUserByGUIDAsync(userGUID);
             if (user is null)
                throw new Exception("User does not exists");
            
            return await identityDirector.GetUserRolesAsync(user);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    
    public async Task<string> GetCurrentUserIdAsync() => await identityDirector.GetLoggedUserGUIDAsync();

  
}