using UserModule.Informations;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Models.Factories;

public class InformationResultFactory
{
    public static InformationResult Success(AppUser user)
    {
        return new InformationResult(user);
    }

    public static InformationResult Failure(Exception ex)
    {
        return new InformationResult(ex);
    }
}