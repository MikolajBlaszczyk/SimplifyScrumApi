namespace UserModule.Security.Models;

public static class SecurityResultsFactory
{
    public static SecurityResult CreateSuccessResult()
    {
        return new SecurityResult();
    }

    public static SecurityResult CreateFailureResult(Exception ex)
    {
        return new SecurityResult(ex);
    }
}