using UserModule.Records;

namespace UserModule.Security.Models;

public class UsersInfoResult : BaseResult
{
    public UsersInfoResult(List<SimpleUserModel> users) { Data = users; }
    public UsersInfoResult(Exception ex): base(ex) { }
    

    public static implicit operator UsersInfoResult(List<SimpleUserModel> model) => new(model);
    public static implicit operator UsersInfoResult(Exception ex) => new(ex);
}