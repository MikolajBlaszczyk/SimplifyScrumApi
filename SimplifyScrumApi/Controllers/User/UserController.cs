using DataAccess.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using UserModule.Abstraction;

namespace SimplifyScrum.Controllers.User;

[ApiController]
[Route("api/v1/scrum/user/")]
[Authorize]
public class UserController(IManageUserInformation infoManager) : ControllerBase
{
    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetUsersInfo()
    {
        var guid = HttpContext.User.GetUserGuid();
        var result  = await infoManager.GetInfoByUserGUIDAsync(guid);

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        
        return StatusCode(500, result.Exception!.Message);
    }

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await infoManager.GetAllUsersAsync();

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return StatusCode(500, result.Exception!.Message);
    }

   
}