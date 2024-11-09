using BacklogModule.Abstraction;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using UserModule.Abstraction;

namespace SimplifyScrum.Controllers.Backlog;

[ApiController]
[Authorize]
[Route("api/v1/scrum/sprint/")]
public class SprintController(IManageSprint sprintManager, IManageUserInformation infoManager) : ControllerBase
{
    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetCurrentSprintInfo()
    {
        var userGuid = HttpContext.User.GetUserGuid();

        var hierarchyResult  = await infoManager.GetUsersProjectAsync(userGuid);

        if (hierarchyResult.IsFailure)
            return StatusCode(500, hierarchyResult.Exception!.Message);
        
        var projectGuid = ((Project)hierarchyResult.Data!).GUID;
        var result = await sprintManager.GetSprintInfoForProject(projectGuid);
        
        return Ok(result);
    }
    
}