using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    [Route("{year}/{month}")]
    public IActionResult GetMeetings([FromQuery] string year, [FromQuery] string month)
    {
        return Ok();
    }
        
    
}