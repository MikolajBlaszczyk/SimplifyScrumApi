using System.Security.Claims;
using DataAccess.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using SimplifyScrum.Utils.Messages;
using SimplifyScrum.Utils.Requests;
using UserModule.Abstraction;
using UserModule.Records;

namespace SimplifyScrum.Controllers.User;

[ApiController]
[Authorize]
[Route("api/v1/scrum/permission/")]
public class PermissionController(IManageUserInformation infoManager, ResultUnWrapper unWrapper) : ControllerBase
{
    private static readonly ResponseProducer _producer = ResponseProducer.Shared;
    
    [HttpGet]
    [Route("po")]
    public async Task<IActionResult> IsProjectOwner()
    {
        var roleString = HttpContext.User.GetScrumClaim();
        if (string.IsNullOrEmpty(roleString) == false)
        {
            var role = (ScrumRole)Convert.ToInt32(roleString);
            return Ok(role == ScrumRole.ProjectOwner);
        }
        
        var user = HttpContext.User.GetUserGuid();

        var result = await infoManager.GetInfoByUserGUIDAsync(user);
        if (result.IsFailure)
            return _producer.InternalServerError(Messages.GenericError500);
        
        var unwrappedUser = unWrapper.Unwrap(result, out SimpleUserModel userModel);

        if (userModel.Role != null)
            await RegisterScrumRoleClaim(userModel.Role.Value);
        
        return Ok(userModel.Role == ScrumRole.ProjectOwner);
    }

    private async Task RegisterScrumRoleClaim(ScrumRole role)
    { 
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        claimsIdentity.AddClaim(new Claim(SimpleClaims.ScrumRoleClaim, role.ToString()));
        await HttpContext.SignInAsync(HttpContext.User);
    }
}