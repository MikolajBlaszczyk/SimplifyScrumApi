using UserModule.Models;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Abstraction;

public interface IManageUserInformation
{
    Task<UserInfoResult> GetInfoByUserGUIDAsync(string guid);
    Task<HierarchyResult> GetUsersProjectAsync(string guid);
    Task<UsersInfoResult> GetAllUsersAsync();
}