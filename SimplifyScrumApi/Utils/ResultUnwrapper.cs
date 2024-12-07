using UserModule.Security.Models;

namespace SimplifyScrum.Utils;

public class ResultUnWrapper
{
    
    private readonly ILogger<ResultUnWrapper> _logger;

    public ResultUnWrapper(ILogger<ResultUnWrapper> logger)
    {
        _logger = logger;
    }
    
    
    public bool Unwrap<T>(BaseResult result, out T unwrapped) where T : class?
    {
        try
        {
            unwrapped = result.Data as T;
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Cannot uwarp the result", e);
            unwrapped = null;
            return false;
        }
    }
}