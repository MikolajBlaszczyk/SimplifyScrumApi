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
        var guid = HttpContext.User.Claims.First(claim => claim.Type == SimpleClaims.UserGuidClaim).Value;
        var result  = await infoManager.GetInfoByUserGuid(guid);

        if (result.IsSuccess)
        {
            return Ok(result.UserModel);
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