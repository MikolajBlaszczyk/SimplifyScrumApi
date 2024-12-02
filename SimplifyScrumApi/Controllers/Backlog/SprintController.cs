using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using SimplifyScrum.Utils.Requests;
using UserModule.Abstraction;

namespace SimplifyScrum.Controllers.Backlog;

[ApiController]
[Authorize]
[Route("api/v1/scrum/sprint/")]
public class SprintController(IManageSprint sprintManager, IManageUserInformation infoManager, ResultUnWrapper unWrapper) : ControllerBase
{
    private readonly ResponseProducer _producer = ResponseProducer.Shared;
    
    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetCurrentSprintInfo()
    {
        var userGuid = HttpContext.User.GetUserGuid();
        if (userGuid is null)
            return _producer.InternalServerError();
        
        var hierarchyResult  = await infoManager.GetUsersProjectAsync(userGuid);
        if (hierarchyResult.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(hierarchyResult, out Project project);
        var result = await sprintManager.GetSprintInfoByProjectGUID(project.GUID);

        if (result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out SprintRecord sprint);
        if(sprint is null)
            return _producer.NoContent();
        
        return Ok(sprint);
    }
    
}