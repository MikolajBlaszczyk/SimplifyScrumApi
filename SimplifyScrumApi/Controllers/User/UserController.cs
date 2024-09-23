using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModule.Abstraction;

namespace SimplifyScrum.Controllers.User;

[ApiController]
[Route("api/v1/scrum/user/")]
[Authorize]
public class UserController(IManageInformation infoManager) : ControllerBase
{
    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetUsersInfo()
    {
        var name = HttpContext.User.Identity.Name;
        var result  = await infoManager.GetInfoByName(name);

        if (result.IsSuccess)
        {
            return Ok(result.User);
        }
        
        return StatusCode(500, result.Exception!.Message);
    }

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await infoManager.GetAllUsers();

        if (result.IsSuccess)
        {
            return Ok(result.Users);
        }

        return StatusCode(500, result.Exception!.Message);
    }
}