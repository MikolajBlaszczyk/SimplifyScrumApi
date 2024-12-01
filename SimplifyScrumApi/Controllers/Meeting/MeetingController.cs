using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulingModule;
using SchedulingModule.Abstraction;
using SchedulingModule.Records;
using SimplifyScrum.Utils;

namespace SimplifyScrum.Controllers.Meeting;

[ApiController]
[Authorize]
[Route("api/v1/scrum/meetings/")]
public class MeetingController(ISchedule scheduler, IManageMeetings meetingsManager) : ControllerBase
{
    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetMeetingsInThisMonthForCurrentUser()
    {
        var guid = HttpContext.User.GetUserGuid();
        var result = await scheduler.GetCurrentMonthSchedule(DateTime.Now, guid);

        if (result.IsSuccess)
        {
            return Ok(result.Data!);
        }
        
        return StatusCode(500, result.Exception!.Message);
    }

    [HttpPost("add")]
    [HttpPut("update")]
    public async Task<IActionResult> AddMeeting([FromBody] MeetingRecord record)
    { 
        var result = await meetingsManager.UpsertMeeting(record);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return StatusCode(500, result.Exception!.Message);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteMeeting([FromBody] MeetingRecord record)
    {
        var result = await meetingsManager.DeleteMeeting(record);

        if (result.IsSuccess)
        {
            return Ok(record.GUID);
        }

        return StatusCode(500, result.Exception!.Message);
    }
    
}