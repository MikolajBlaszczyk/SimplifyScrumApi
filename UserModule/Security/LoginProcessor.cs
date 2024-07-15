using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using UserModule.Exceptions;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Validation;

namespace UserModule.Security;

public class LoginProcessor 
{
    private readonly UserValidator validator;
    private readonly AspIdentityDirector identityDirector;
    private readonly SignInManager<Teammate> signInManager;

    public LoginProcessor(UserValidator validator, AspIdentityDirector identityDirector)
    {
        this.validator = validator;
        this.identityDirector = identityDirector;
    }
   
    public async Task<SecurityResult> LoginUser(AppUser user)
    {
        try
        {
            var validation = validator.ValidateBeforeLogin(user);
            if (validation.IsFailure)
                throw new InternalValidationException(validation.Message);
            
            await identityDirector.Login(user);
        }
        catch (Exception ex)
        {
            return SecurityResultsFactory.CreateFailureResult(ex);
        }

        return SecurityResultsFactory.CreateSuccessResult();
    }
}