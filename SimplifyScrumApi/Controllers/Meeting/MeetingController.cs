using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulingModule;
using SchedulingModule.Records;

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
        var name = HttpContext.User.Identity.Name;
        var currentDate = DateTime.Now;
        var result = await scheduler.GetScheduleByMonthForCurrentUser(currentDate, name);

        if (result.IsSuccess)
        {
            return Ok(result.ScheduleRecord!);
        }
        
        return StatusCode(500, result.Exception!.Message);
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddMeeting([FromBody] MeetingRecord record)
    {
        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public IActionResult UpdateMeeting([FromBody] MeetingRecord record)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult DeleteMeeting([FromBody] MeetingRecord record)
    {
        return Ok();
    }
    
}