using Microsoft.AspNetCore.Http;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Models.Converters;
using UserModule.Security.Validation;

namespace UserModule;

public class UserAccountProcessor
{
    private readonly UserValidator validator;
    private readonly UserModelConverter converter;
    private readonly AspIdentityDirector identityDirector;

    public UserAccountProcessor(
        IHttpContextAccessor contextAccessor,
        UserValidator validator,
        UserModelConverter converter,
        AspIdentityDirector identityDirector)
    {
        this.validator = validator;
        this.converter = converter;
        this.identityDirector = identityDirector;
    }

    public async Task<SecurityResult> SignInUser(SimpleUserModel userModel)
    {
        try
        {
            var validation = validator.ValidateBeforeSignIn(userModel);
            if (validation.IsFailure)
                throw new Exception(validation.Message);

            var teammate = converter.ConvertToTeammate(userModel);
            await identityDirector.CreateUser(teammate, userModel.Password);
        }
        catch (Exception e)
        {
            return SecurityResultsFactory.Failure(e);
        }

        return SecurityResultsFactory.Success();
    }

    public async Task<SecurityResult> DeleteCurrentUser()
    {
        try
        {
            var userGuid = await identityDirector.GetLoggedUserGUID();
            identityDirector.DeleteUserByGuid(userGuid);
        }
        catch (Exception ex)
        {
            return SecurityResultsFactory.Failure(ex);
        }

        return SecurityResultsFactory.Success();
    }

    public async Task<string> GetCurrentUserId() => await identityDirector.GetLoggedUserGUID();
   
}