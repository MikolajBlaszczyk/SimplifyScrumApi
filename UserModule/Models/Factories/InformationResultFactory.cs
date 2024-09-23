using UserModule.Informations;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Models.Factories;

public class InformationResultFactory
{
    public static InformationResult Success(List<SimpleUserModel> users)
    {
        return new InformationResult(users);
    }
    
    public static InformationResult Success(SimpleUserModel userModel)
    {
        return new InformationResult(userModel);
    }

    public static InformationResult Failure(Exception ex)
    {
        return new InformationResult(ex);
    }
}