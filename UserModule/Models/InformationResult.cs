using UserModule.Records;

namespace UserModule.Security.Models;

public class InformationResult : BaseResult
{
    public InformationResult(AppUser user) 
    {
        User = user;
    }

    public InformationResult(List<AppUser> users)
    {
        
    } 
    
    public InformationResult(Exception ex): base(ex)
    {
        User = null;
    }
    
    public AppUser? User { get; set; }
    public List<AppUser> Users { get; set; }
}