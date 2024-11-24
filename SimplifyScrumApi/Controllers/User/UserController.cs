using DataAccess.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using UserModule.Abstraction;
using UserModule.Records;

namespace SimplifyScrum.Controllers.User;

[ApiController]
[Route("api/v1/scrum/")]
[Authorize]
public class UserController(IManageUserInformation infoManager) : ControllerBase
{
    [HttpGet]
    [Route("user/info")]
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
    

    [HttpPost]
    [Route("user/update")]
    public async Task<IActionResult> UpdateUser([FromBody] SimpleUserModel user)
    {
        var result = await infoManager.UpdateUserInfo(user);

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        
        return StatusCode(500, result.Exception!.Message);
    }
    
    [HttpGet]
    [Route("user/users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await infoManager.GetAllUsersAsync();

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return StatusCode(500, result.Exception!.Message);
    }

    [HttpGet]
    [Route("user/team/members")]
    public async Task<IActionResult> GetTeamMembers()
    {
        var guid = HttpContext.User.GetUserGuid();
        var result = await infoManager.GetInfoByUserGUIDAsync(guid);
        var members = await infoManager.GetTeamMemebers(result.Data.TeamGuid);

        return Ok(members.Data);
    }
    
    [HttpGet]
    [Route("teams")]
    public async Task<IActionResult> GetAllTeams()
    {
        var result = await infoManager.GetAllTeamsAsync();

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        
        return StatusCode(500, result.Exception!.Message);
    }
    
    [HttpGet]
    [Route("team")]
    public async Task<IActionResult> GetTeam([FromQuery] string teamGUID)
    {
        var result = await infoManager.GetTeam(teamGUID);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
       
        return Ok();
    }
    
    [HttpPost]
    [Route("team/add")]
    public async Task<IActionResult> AddTeam([FromBody] SimpleTeamModel model)
    {
        var result = await infoManager.AddTeam(model);
        if (result.IsSuccess)
        {
            var user = await infoManager.GetInfoByUserGUIDAsync(model.ManagerGUID);
            await infoManager.AddUsersToTeam(new List<SimpleUserModel>{ user.Data }, result.Data);
        }
       
        return Ok();
    }

   
}