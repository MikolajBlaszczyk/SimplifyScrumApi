using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using DataAccess.Abstraction;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using UserModule.Abstraction;
using UserModule.Records;

namespace SimplifyScrum.Controllers.Backlog;

[ApiController]
[Authorize]
[Route("api/v1/scrum")]
public class ProjectItemController(IManageUserInformation userInfoManager, IManageTask projectManager): ControllerBase
{
    [HttpGet]
    [Route("projects")]
    public async Task<IActionResult> GetAllProjects()
    {
        var guidClaim = User.Claims.FirstOrDefault(claim => claim.Type == SimpleClaims.UserGuidClaim);
        if (guidClaim is null)
            return StatusCode(500, "Server error");

        var infoResult = await userInfoManager.GetInfoByUserGUIDAsync(guidClaim.Value);
        if (infoResult.IsFailure)
            return StatusCode(500, "Server error");

        //var projects  = await projectManager.GetAllProjectsForTeam(infoResult.Data!.TeamGuid);

        return Ok(); //projects);
    }

    [HttpGet]
    [Route("project")]
    public async Task<IActionResult> GetProject([FromQuery] string projectGUID)
    {
        // var project = await projectManager.GetProjectByGuid(projectGUID);
        // return Ok(project.Data);
        return Ok();
    }
    

    [HttpDelete]
    [Route("project/delete")]
    public async Task<IActionResult> DeleteProject([FromQuery] string projectGUID)
    {
        //await projectManager.DeleteProject(projectGUID);

        return Ok();
    }

    [HttpPost]
    [Route("project/add")]
    public async Task<IActionResult> AddProject([FromBody] ProjectRecord record)
    {
        //await projectManager.AddProject(record);

        return Ok();
    }
    
    
    [HttpGet]
    [Route("project/features")]
    public async Task<IActionResult> GetFeaturesByProjectGUID([FromQuery] string projectGUID)
    {
        //var features = await projectManager.GetAllFeaturesByProjectGUID(projectGUID);
        
        //return Ok(features);
        return Ok();
    }

    [HttpGet]
    [Route("feature")]
    public async Task<IActionResult> GetFeature([FromQuery] string featureGUID)
    {
        // var result = await projectManager.GetFeatureByGUID(featureGUID);
        // return Ok(result.Data); 
        return Ok();
    }

    [HttpPost]
    [Route("feature/add")]
    public async Task<IActionResult> AddFeature([FromBody] FeatureRecord record)
    {
        // await projectManager.AddFeature(record);
        //
        // return Ok();
        return Ok();
    }

    [HttpDelete]
    [Route("feature/delete")]
    public async Task<IActionResult> DeleteFeature([FromQuery] string featureGUID)
    {
        // await projectManager.DeleteFeature(featureGUID);
        //
        // return Ok();
        return Ok();
    }
    
    [HttpGet]
    [Route("features/tasks")]
    public async Task<IActionResult> GetTasksByFeatureGUID([FromQuery] string featureGUID)
    {
        var tasks = await projectManager.GetAllTasksByFeatureGUID(featureGUID);

        return Ok(tasks);
    }

    [HttpGet]
    [Route("task")]
    public async Task<IActionResult> GetTask([FromQuery] int taskID)
    {
        var result = await projectManager.GetTaskById(taskID);

        return Ok(result.Data);
    }
    
    [HttpPost]
    [Route("task/add")]
    public async Task<IActionResult> AddTask([FromBody] TaskRecord record)
    {
        await projectManager.AddTask(record);

        return Ok();
    }

    [HttpDelete]
    [Route("task/delete")]
    public async Task<IActionResult> DeleteTask([FromQuery] int taskID)
    {
        await projectManager.DeleteTask(taskID);

        return Ok();
    }

   
}