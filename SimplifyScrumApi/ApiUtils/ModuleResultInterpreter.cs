using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserModule.Exceptions;
using UserModule.Security.Models;

namespace SimplifyScrum.ApiUtils;

public class ModuleResultInterpreter 
{
    public ObjectResult InterpretSecurityResult(SecurityResult securityResult)
    {
        
        return securityResult switch
        {
            {IsSuccess: true } => new OkObjectResult("Success"),
            {IsFailure: true, Exception: InternalIdentityException }  =>  new BadRequestObjectResult(securityResult.Exception.Message),
            {IsFailure: true, Exception: InternalValidationException } => new BadRequestObjectResult(securityResult.Exception.Message), 
            _ => new ObjectResult(securityResult.Exception!.Message) {StatusCode = 500}
        };
    }
}