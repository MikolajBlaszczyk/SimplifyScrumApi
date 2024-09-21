namespace UserModule.Security.Models;

public class BaseResult
{

    public BaseResult()
    {
        IsSuccess = true;
        IsFailure = false;
        Exception = null;
    }

    public BaseResult(Exception ex)
    {
        IsSuccess = false;
        IsFailure = true;
        Exception = ex;
    }
    
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get; private set; }
    public Exception? Exception { get; private set; }
}