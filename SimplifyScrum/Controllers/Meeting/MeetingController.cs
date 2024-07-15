using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimplifyScrum.Controllers.Meeting;

[ApiController]
[Authorize]
[Route("api/v1/scrum/meetings")]
public class MeetingController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllMeetings()
    {
        return Ok();
    }
    
}