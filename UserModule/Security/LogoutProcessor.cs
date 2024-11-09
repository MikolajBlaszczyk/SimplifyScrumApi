using UserModule.Security.Models;

namespace UserModule;

public class LogoutProcessor(AspIdentityDirector identityDirector)
{
    public async Task<SecurityResult> LogoutCurrentUserAsync()
    {
        try
        {
            await identityDirector.LogoutAsync();
        }
        catch (Exception ex)
        {
            return ex;
        }

        return SecurityResult.SuccessWithoutData();
    }
}