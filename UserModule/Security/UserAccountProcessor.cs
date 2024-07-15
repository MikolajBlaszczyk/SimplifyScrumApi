using System.Security;
using System.Security.Claims;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Http;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Models.Converters;
using UserModule.Security.Validation;

namespace UserModule;

public class UserAccountProcessor
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserValidator validator;
    private readonly UserModelConverter converter;
    private readonly AspIdentityDirector identityDirector;

    public UserAccountProcessor(
        IHttpContextAccessor contextAccessor,
        UserValidator validator,
        UserModelConverter converter,
        AspIdentityDirector identityDirector)
    {
        this._contextAccessor = contextAccessor;
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

            await identityDirector.CreateUser(teammate);
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
            var teammateGuid = GetCurrentUserGuid();
            var user = await identityDirector.GetUserByGuid(teammateGuid);
            identityDirector.DeleteUser(user);

        }
        catch (Exception ex)
        {
            return SecurityResultsFactory.CreateFailureResult(ex);
        }

        return SecurityResultsFactory.CreateSuccessResult();
    }

    private string? GetCurrentUserGuid()
    {
        return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}