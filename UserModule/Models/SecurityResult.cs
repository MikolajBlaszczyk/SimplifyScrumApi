namespace UserModule.Security.Models;

public class SecurityResult : BaseResult
{
    public SecurityResult() { }
    public SecurityResult(string userGUID) { Data = userGUID; }
    public SecurityResult(IList<string> roles) { Data = roles; }
    public SecurityResult(Exception ex) : base(ex) {  }
    
    public static SecurityResult SuccessWithoutData() => new();
    
    public static implicit operator SecurityResult(string userGUID) => new(userGUID);
    public static implicit operator SecurityResult(Exception ex) => new(ex);
    public static implicit operator SecurityResult(List<string> roles) => new(roles);
}