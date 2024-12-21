using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulingModule.Abstraction;
using SchedulingModule.Records;
using SimplifyScrum.Utils;
using SimplifyScrum.Utils.Messages;
using SimplifyScrum.Utils.Requests;

namespace SimplifyScrum.Controllers.Meeting;

[ApiController]
[Authorize]
[Route("api/v1/scrum/meetings/")]
public class MeetingController(ISchedule scheduler, IScheduleMeetings meetingsManager, ResultUnWrapper unWrapper) : ControllerBase
{
    private ResponseProducer _producer = ResponseProducer.Shared;
    
    
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

    [HttpGet]
    [Route("meeting")]
    public async Task<IActionResult> GetMeetingById([FromQuery] string meetingGuid)
    {
        var result = await meetingsManager.GetMeeting(meetingGuid);

        if (result.IsFailure)
            return _producer.InternalServerError(Messages.GenericError500);

        return Ok(result.Data);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMeeting([FromBody] MeetingRecord record)
    { 
        var meetingAddResult = await meetingsManager.AddMeeting(record);

        if (meetingAddResult.IsFailure)
            return _producer.InternalServerError(Messages.GenericError500);


        unWrapper.Unwrap(meetingAddResult, out MeetingRecord meeting);
        
        List<string> usersToLink = new List<string>();
        if(record.UserGuids != null)
            usersToLink.AddRange(record.UserGuids);
        
        if(usersToLink.Contains(record.LeaderGuid) == false)
            usersToLink.Add(record.LeaderGuid);
        
        var linkingResult = await meetingsManager.LinkUsers(meeting, usersToLink);
        
        if (linkingResult.IsFailure)
            return _producer.InternalServerError(Messages.GenericError500);

        return Ok(meeting);
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateMeeting([FromBody] MeetingRecord record)
    { 
        var meetingAddResult = await meetingsManager.UpdateMeeting(record);

        if (meetingAddResult.IsFailure)
            return _producer.InternalServerError(Messages.GenericError500);


        unWrapper.Unwrap(meetingAddResult, out MeetingRecord meeting);
        
        List<string> usersToLink = new List<string>();
        if(record.UserGuids != null)
            usersToLink.AddRange(record.UserGuids);
        
        usersToLink.Add(record.LeaderGuid);
        
        var linkingResult = await meetingsManager.LinkUsers(meeting, usersToLink);
        
        if (linkingResult.IsFailure)
            return _producer.InternalServerError(Messages.GenericError500);

        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteMeeting([FromQuery] string guid)
    {
        var result = await meetingsManager.DeleteMeeting(guid);

        if (result.IsSuccess)
        {
            return Ok(guid);
        }

        return StatusCode(500, result.Exception!.Message);
    }
    
}