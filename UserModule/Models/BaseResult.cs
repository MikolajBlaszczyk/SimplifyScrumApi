namespace UserModule.Security.Models;

public class BaseResult
{
    public BaseResult()
    {
        IsSuccess = true;
        IsFailure = false;
        Exception = null;
        Data = null;
    }
    
    public BaseResult(Exception ex)
    {
        IsSuccess = false;
        IsFailure = true;
        Exception = ex;
        Data = null;
    }
    
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get; private set; }
    public Exception? Exception { get; private set; }
    public dynamic? Data { get;  set; }

    public static implicit operator BaseResult(Exception ex)
    {
        var result = new BaseResult(ex);
        return result;
    }
    
  
}