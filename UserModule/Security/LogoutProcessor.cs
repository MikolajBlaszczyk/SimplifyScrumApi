using UserModule.Security.Models;

namespace UserModule;

public class LogoutProcessor
{
    private readonly AspIdentityDirector identityDirector;

    public LogoutProcessor(AspIdentityDirector identityDirector)
    {
        this.identityDirector = identityDirector;
    }
    
    public async Task<SecurityResult> LogoutCurrentUser()
    {
        try
        {
            await identityDirector.Logout();
        }
        catch (Exception ex)
        {
            return SecurityResultsFactory.Failure(ex);
        }

        return SecurityResultsFactory.Success();
    }
}