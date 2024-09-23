namespace UserModule.Security.Models;

public class SecurityResult : BaseResult
{
    public SecurityResult()
    {
        UserGUID = null;
    }

    public SecurityResult(string userGUID)
    {
        UserGUID = userGUID;
    }
    
    public SecurityResult(Exception ex) : base(ex)
    {
        UserGUID = null;
    }
    
    public string? UserGUID { get; set; }
}