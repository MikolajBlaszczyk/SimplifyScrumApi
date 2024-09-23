using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulingModule;
using SchedulingModule.Records;
using SimplifyScrum.Utils;

namespace SimplifyScrum.Controllers.Meeting;

[ApiController]
[Authorize]
[Route("api/v1/scrum/meetings/")]
public class MeetingController(Scheduler scheduler) : ControllerBase
{
    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetMeetingsInThisMonthForCurrentUser()
    {
        var guid = HttpContext.User.GetUserGuid();
        var result = await scheduler.GetScheduleByMonthForCurrentUser(DateTime.Now, guid);

        if (result.IsSuccess)
        {
            return Ok(result.ScheduleRecord!);
        }
        
        return StatusCode(500, result.Exception!.Message);
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddMeeting([FromBody] SimpleMeetingModel model)
    {
        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public IActionResult UpdateMeeting([FromBody] SimpleMeetingModel model)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult DeleteMeeting([FromBody] SimpleMeetingModel model)
    {
        return Ok();
    }
    
}