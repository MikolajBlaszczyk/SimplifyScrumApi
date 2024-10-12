using UserModule.Records;

namespace UserModule.Security.Models;

public class InformationResult : BaseResult
{
    public InformationResult(SimpleUserModel userModel) 
    {
        UserModel = userModel;
    }

    public InformationResult(List<SimpleUserModel> users)
    {
        Users = users;
    } 
    
    public InformationResult(Exception ex): base(ex)
    {
        UserModel = null;
    }
    
    public SimpleUserModel? UserModel { get; set; }
    public List<SimpleUserModel> Users { get; set; }
}