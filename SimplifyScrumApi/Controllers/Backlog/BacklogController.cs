using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using DataAccess.Model.User;
using DataAccess.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimplifyScrum.Utils;
using SimplifyScrum.Utils.Messages;
using SimplifyScrum.Utils.Requests;
using UserModule.Abstraction;
using UserModule.Records;

namespace SimplifyScrum.Controllers.Backlog;

[ApiController]
[Authorize]
[Route("api/v1/scrum/")]
public class BacklogController(IManageUserInformation userInfoManager, IManageBacklog backlogManager, ResultUnWrapper unWrapper): ControllerBase
{
    private readonly ResponseProducer _producer = ResponseProducer.Shared;
    
    [HttpGet]
    [Route("projects")]
    public async Task<IActionResult> GetAllProjects()
    {
        var guid = HttpContext.User.GetUserGuid();
        if (guid is null)
            return _producer.InternalServerError();

        var infoResult = await userInfoManager.GetInfoByUserGUIDAsync(guid);
        if (infoResult.IsFailure)
            return _producer.InternalServerError();

        var unwrappedUser = unWrapper.Unwrap(infoResult, out SimpleUserModel user);
        if(unwrappedUser is false)
            return _producer.InternalServerError();
        
        
        var projectResult  = await backlogManager.GetTeamProjects(user.TeamGuid);
        if(projectResult.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(projectResult, out List<ProjectRecord> projects);
        if (projects.Count == 0)
            return _producer.NoContent();
        
        return Ok(projectResult.Data);
    }

    //TODO: Write UT for this
    [HttpGet]
    [Route("project/active")]
    public async Task<IActionResult> GetActiveProject()
    {
        var teamGuid = HttpContext.User.GetTeamGuid();
        if (teamGuid.IsNullOrEmpty())
            return _producer.BadRequest(Messages.UserIsNotInAnyTeam);
        
        var result = await backlogManager.GetProjectByTeam(teamGuid);

        if (result.IsFailure)
            return _producer.InternalServerError(Messages.GenericError500);
        
        unWrapper.Unwrap(result, out ProjectRecord? project);
        if (project is null)
            return _producer.NoContent();
        
        return Ok(project);
    }
    

    [HttpGet]
    [Route("project")]
    public async Task<IActionResult> GetProject([FromQuery] string projectGUID)
    {
        if (string.IsNullOrEmpty(projectGUID))
            return _producer.BadRequest(Messages.MissingRequiredValue(Messages.GetProjectParams));
        
        var result = await backlogManager.GetProjectByGuid(projectGUID);
        if (result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out ProjectRecord? project);
        if(project is null)
            return _producer.NoContent();
        
        return Ok(project);
    }
    
    
    

    [HttpDelete]
    [Route("project/delete")]
    public async Task<IActionResult> DeleteProject([FromQuery] string projectGUID)
    {
        if (string.IsNullOrEmpty(projectGUID))
            return _producer.BadRequest(Messages.MissingRequiredValue(Messages.DeleteProjectParams));
        
        var result = await backlogManager.DeleteProject(projectGUID);
        if(result.IsFailure && result.Exception is AccessorException)
            return _producer.BadRequest(Messages.ActionFailed);
        if(result.IsFailure)
            return _producer.InternalServerError(Messages.ActionFailed);

        unWrapper.Unwrap(result, out ProjectRecord? record);
        if(record is null)
            return _producer.InternalServerError(Messages.ActionFailed);

        return Ok(record);
    }

    [HttpPost]
    [Route("project/add")]
    public async Task<IActionResult> AddProject([FromBody] ProjectRecord record)
    {
        var result = await backlogManager.AddProject(record);
        if(result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out ProjectRecord? project);
        if(project is null)
            return _producer.InternalServerError(Messages.ActionFailed);
        
        return Ok(project) ;
    }
    
    
    [HttpGet]
    [Route("project/features")]
    public async Task<IActionResult> GetFeaturesByProjectGUID([FromQuery] string projectGUID)
    {
        if (string.IsNullOrEmpty(projectGUID))
            return _producer.BadRequest(Messages.MissingRequiredValue(Messages.GetFeaturesByProjectGUIDParams));
        
        var result = await backlogManager.GetProjectFeatures(projectGUID);
        if(result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out List<FeatureRecord> features);
        if(features.Count == 0)
            return _producer.NoContent();

        return Ok(features);
    }

    [HttpGet]
    [Route("feature")]
    public async Task<IActionResult> GetFeature([FromQuery] string featureGUID)
    {
        if (string.IsNullOrEmpty(featureGUID))
            return _producer.BadRequest(Messages.MissingRequiredValue(Messages.GetFeatureParams));
        
        var result = await backlogManager.GetFeatureByGuid(featureGUID);
        if(result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out FeatureRecord? feature);
        if(feature is null)
            return _producer.NoContent();
        
        return Ok(feature);
    }

    [HttpPost]
    [Route("feature/add")]
    public async Task<IActionResult> AddFeature([FromBody] FeatureRecord record)
    {
        var result = await backlogManager.AddFeature(record);
        if(result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out FeatureRecord? feature);
        if(feature is null)
            return _producer.InternalServerError(Messages.ActionFailed);
        
        return Ok(feature);
    }

    [HttpDelete]
    [Route("feature/delete")]
    public async Task<IActionResult> DeleteFeature([FromQuery] string featureGUID)
    {
        if (string.IsNullOrEmpty(featureGUID))
            return _producer.BadRequest(Messages.MissingRequiredValue(Messages.DeleteFeatureParams));
        
        var result = await backlogManager.DeleteFeature(featureGUID);
        if(result.IsFailure && result.Exception is AccessorException)
            return _producer.BadRequest(Messages.ActionFailed);
        if(result.IsFailure)
            return _producer.InternalServerError(Messages.ActionFailed);
        
        unWrapper.Unwrap(result, out FeatureRecord? record);
        if(record is null)
            return _producer.InternalServerError(Messages.ActionFailed);
        
        return Ok(record);
    }
    
    [HttpGet]
    [Route("feature/tasks")]
    public async Task<IActionResult> GetTasksByFeatureGUID([FromQuery] string featureGUID)
    {
        if (string.IsNullOrEmpty(featureGUID))
            return _producer.BadRequest(Messages.MissingRequiredValue(Messages.GetTasksByFeatureGUIDParams));
        
        var result = await backlogManager.GetFeatureTasks(featureGUID);
        if(result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out List<TaskRecord> tasks);
        if(tasks.Count == 0)
            return _producer.NoContent();
        
        return Ok(tasks);
    }

    [HttpGet]
    [Route("task")]
    public async Task<IActionResult> GetTask([FromQuery] int taskID)
    {
        var result = await backlogManager.GetTaskById(taskID);
        if(result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out TaskRecord? task);
        if(task is null)
            return _producer.NoContent();
        
        return Ok(task);
    }
    
    [HttpPost]
    [Route("task/add")]
    public async Task<IActionResult> AddTask([FromBody] TaskRecord record)
    {
        var result = await backlogManager.AddTask(record);
        if(result.IsFailure)
            return _producer.InternalServerError();
        
        unWrapper.Unwrap(result, out TaskRecord? task);
        if(task is null)
            return _producer.InternalServerError(Messages.ActionFailed);
        
        return Ok(task);
    }

    [HttpDelete]
    [Route("task/delete")]
    public async Task<IActionResult> DeleteTask([FromQuery] int taskID)
    {
        var result = await backlogManager.DeleteTask(taskID);

        if(result.IsFailure && result.Exception is AccessorException)
            return _producer.BadRequest(Messages.ActionFailed);
        if(result.IsFailure)
            return _producer.InternalServerError(Messages.ActionFailed);
        
        unWrapper.Unwrap(result, out TaskRecord? record);
        if(record is null)
            return _producer.InternalServerError(Messages.ActionFailed);
        
        return Ok(record);
    }

   
}