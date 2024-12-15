using System.IdentityModel.Tokens.Jwt;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UserModule.Exceptions;
using UserModule.Records;
using UserModule.Security.Models;
using UserModule.Security.Validation;

namespace UserModule.Security;

public class LoginProcessor (UserValidator validator, AspIdentityDirector identityDirector)
{
   
    public async Task<SecurityResult> LoginUserAsync(SimpleUserModel userModel)
    {
        try
        {
            var validation = validator.ValidateBeforeLogin(userModel);
            if (validation.IsFailure)
                throw new InternalValidationException(validation.Message);
            
            var loginSucceeded = await identityDirector.LoginAsync(userModel);
            
            if (loginSucceeded == false)
                throw new Exception("Wrong username or password. Please provide correct credentials.");
        }
        catch (Exception ex)
        {
            return ex;
        }

        return SecurityResult.SuccessWithoutData();
    }
}