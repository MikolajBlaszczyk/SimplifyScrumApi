using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.ApiUtils;
using UserModule.Abstraction;

namespace SimplifyScrum.Controllers.User;

[ApiController]
[Route("api/v1/scrum/user")]
public class UserController(IManageInformation infoManager, ModuleResultInterpreter resultInterpreter)
{
    [HttpGet]
    [Route("/info")]
    public async Task<IActionResult> GetUsersInfo([FromQuery] string guid)
    {
        var infoResult  = await infoManager.GetInfoByGUID(guid);

        return resultInterpreter.InterpretInformationResult(infoResult);
    }
}