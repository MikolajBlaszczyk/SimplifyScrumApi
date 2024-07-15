using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.ApiUtils;
using UserModule.Abstraction;
using UserModule.Records;

namespace SimplifyScrum.Controllers.User;

[ApiController]
[Route("api/v1/scrum/")]
public class LoginController(IManageSecurity securityManager, ModuleResultInterpreter resultInterpreter) : ControllerBase
{
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] AppUser user)
    {
        var result = await securityManager.Login(user);
        
        return resultInterpreter.InterpretSecurityResult(result);
    }

    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await securityManager.Logout();

        return resultInterpreter.InterpretSecurityResult(result);
    }

    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] AppUser user)
    {
        var result = await securityManager.SignIn(user);

        return resultInterpreter.InterpretSecurityResult(result);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete()
    {
        var result = await securityManager.Delete();

        return resultInterpreter.InterpretSecurityResult(result);
    }
}