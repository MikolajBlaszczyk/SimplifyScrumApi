using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulingModule.Records;

namespace SimplifyScrum.Controllers.Meeting;

[ApiController]
[Authorize]
[Route("api/v1/scrum/meetings/")]
public class MeetingController : ControllerBase
{
    [HttpGet]
    [Route("current")]
    public IActionResult GetCurrentMonthMeetings()
    {
        return Ok();
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