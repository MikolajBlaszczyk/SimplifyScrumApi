namespace UserModule.Security.Models;

public class SecurityResult
{
    public SecurityResult()
    {
        IsSuccess = true;
        IsFailure = false;
        Exception = null;
    }

    public SecurityResult(Exception ex)
    {
        IsSuccess = false;
        IsFailure = true;
        Exception = ex;
    }
    
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get; private set; }
    public Exception? Exception { get; private set; }
}