namespace UserModule.Security.Models;

public class SecurityResult : BaseResult
{
    public SecurityResult() : base()
    {
        UserGUID = null;
    }
    
    public SecurityResult(string userGuid) : base()
    {
        UserGUID = userGuid;
    }
    
    public SecurityResult(Exception ex) : base(ex)
    {
        UserGUID = null;
    }
    
    public string? UserGUID { get; set; }
}