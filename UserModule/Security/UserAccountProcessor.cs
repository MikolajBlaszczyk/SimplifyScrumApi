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

    public async Task<SecurityResult> SignInUser(AppUser user)
    {
        try
        {
            var validation = validator.ValidateBeforeSignIn(user);
            if (validation.IsFailure)
                throw new Exception(validation.Message);

            var teammate = converter.ConvertToTeammate(user);
            await identityDirector.CreateUser(teammate, user.Password);
        }
        catch (Exception e)
        {
            return SecurityResultsFactory.CreateFailureResult(e);
        }

        return SecurityResultsFactory.CreateSuccessResult();
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
            return SecurityResultsFactory.CreateFailureResult(ex);
        }

        return SecurityResultsFactory.CreateSuccessResult();
    }
}