using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Abstraction;

public interface IManageInformation
{
    Task<InformationResult> GetInfoByName(string name);

    Task<InformationResult> GetAllUsers();
}