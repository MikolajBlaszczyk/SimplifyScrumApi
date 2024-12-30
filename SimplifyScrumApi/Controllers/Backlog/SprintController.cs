using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using SimplifyScrum.Utils.Messages;
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
        var userGuid = User.GetUserGuid();
        if (userGuid is null)
            return _producer.InternalServerError();
        
        var hierarchyResult  = await infoManager.GetUsersActiveProjectAsync(userGuid);
        if (hierarchyResult.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(hierarchyResult, out Project project);
        if (project is null)
            return _producer.NoContent();
        
        var result = await sprintManager.GetSprintInfoByProjectGUID(project.GUID);

        if (result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out SprintRecord sprint);
        if(sprint is null)
            return _producer.NoContent();
        
        return Ok(sprint);
    }


    //TODO: Write UT 
    [HttpPost]
    [Route("plan")]
    public async Task<IActionResult> PlanSprint(PlanSprintRecord plan)
    {
        if (plan.FeatureGUIDs.Count == 0)
            return _producer.BadRequest(Messages.PlanSprintParams);

        var result = await sprintManager.PlanSprint(plan);

        if (result.IsFailure)
            return _producer.BadRequest(Messages.GenericError500);

        unWrapper.Unwrap(result, out SprintRecord record);
        
        return Ok(record);
    }

    //TODO: Write UT 
    [HttpPost]
    [Route("rate")]
    public async Task<IActionResult> RateSprint(SprintNoteRecord record)
    {
        //TODO: Validation
        var result = await sprintManager.RateSprint(record);
        if(result.IsFailure)
            return _producer.InternalServerError();

        unWrapper.Unwrap(result, out SprintRecord sprintRecord);
        
        return Ok(sprintRecord);
    }

    [HttpGet]
    [Route("items")]
    public async Task<IActionResult> GetItemsForActiveSprint()
    {

        var userGuid = User.GetUserGuid();
        if (userGuid is null)
            return _producer.InternalServerError();
        
        var hierarchyResult  = await infoManager.GetUsersActiveProjectAsync(userGuid);
        if (hierarchyResult.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(hierarchyResult, out Project project);
        if (project is null)
            return _producer.NoContent();
        
        var result = await sprintManager.GetActiveItemsForSprint(project.GUID);
        unWrapper.Unwrap(result, out List<FeatureRecord> feature);
        if(feature is null)
            return _producer.NoContent();
        
        return Ok(feature);
    }
}

