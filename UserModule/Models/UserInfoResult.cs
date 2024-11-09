using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Models;

public class UserInfoResult : BaseResult
{
    public UserInfoResult(SimpleUserModel userModel) { Data = userModel; }
    public UserInfoResult(Exception ex): base(ex) { }
    public SimpleUserModel? Data { get; private set; }

    public static implicit operator UserInfoResult(SimpleUserModel model) => new(model);
    public static implicit operator UserInfoResult(Exception ex) => new(ex);
}