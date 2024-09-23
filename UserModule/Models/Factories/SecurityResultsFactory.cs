namespace UserModule.Security.Models;

public static class SecurityResultsFactory
{
    public static SecurityResult Success()
    {
        return new SecurityResult();
    }

    public static SecurityResult Success(string userGuid)
    {
        return new SecurityResult(userGuid);
    }
    
    public static SecurityResult Failure(Exception ex)
    {
        return new SecurityResult(ex);
    }
}