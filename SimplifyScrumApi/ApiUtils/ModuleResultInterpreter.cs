using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserModule.Exceptions;
using UserModule.Security.Models;

namespace SimplifyScrum.ApiUtils;

public class ModuleResultInterpreter 
{
    public ObjectResult InterpretSecurityResult(SecurityResult result)
    {
        return result switch
        {
            {IsSuccess: true } => new OkObjectResult(result.UserGUID),
            {IsFailure: true, Exception: InternalIdentityException }  =>  new BadRequestObjectResult(result.Exception.Message),
            {IsFailure: true, Exception: InternalValidationException } => new BadRequestObjectResult(result.Exception.Message), 
            _ => new ObjectResult(result.Exception!.Message) {StatusCode = 500}
        };
    }

    public ObjectResult InterpretInformationResult(InformationResult result)
    {
        return result switch
        {
            { IsSuccess: true } => new OkObjectResult(result.User),
            _ => new ObjectResult(result.Exception!.Message) { StatusCode = 500 }
        };
    }
}