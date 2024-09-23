using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Abstraction;

public interface IManageUserInformation
{
    Task<InformationResult> GetInfoByUserGuid(string guid);

    Task<InformationResult> GetAllUsers();
}