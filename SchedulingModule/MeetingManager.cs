using DataAccess.Abstraction;
using DataAccess.Models.Factories;
using SchedulingModule.Abstraction;
using SchedulingModule.Models;
using SchedulingModule.Models.Factories;
using SchedulingModule.Records;
using SchedulingModule.Utils;
using SchedulingModule.Utils.Extensions;

namespace SchedulingModule;

public class MeetingManager(IMeetingAccessor meetingAccessor, TeammateLinker linker): IManageMeetings
{
    public async Task<ScheduleResult> UpsertMeeting(SimpleMeetingModel simpleMeeting)
    {
        var meeting = meetingAccessor.GetMeetingById(simpleMeeting.Identifier);
        
        if(meeting is not null)
        {
            meeting.Update(simpleMeeting);
            
        }
        else
        {
            meeting = MeetingFactory.CreateMeetingWithGuid(
                simpleMeeting.Identifier,
                simpleMeeting.Name,
                simpleMeeting.Description,
                simpleMeeting.LeaderIdentifier,
                simpleMeeting.Start,
                simpleMeeting.Duration,
                simpleMeeting.Type );
        }

        var usersLinked = linker.LinkUsersToMeeting(simpleMeeting, meeting);
        if (usersLinked == false)
            return ScheduleResultFactory.Failure(new Exception("Cannot link users"));
        
        var result = meetingAccessor.UpsertMeeting(meeting);

        if (result is null)
            return ScheduleResultFactory.Failure(new Exception("Could not upsert the meeting"));

        return ScheduleResultFactory.Success();
    }

    public async Task<ScheduleResult> DeleteMeeting(SimpleMeetingModel simpleMeetingToDelete)
    {
        var meeting = meetingAccessor.GetMeetingById(simpleMeetingToDelete.Identifier);

        if (meeting is null)
            return ScheduleResultFactory.Failure(new Exception("Meeting does not exists"));
        
        var deletedMeeting = meetingAccessor.DeleteMeeting(meeting);
        
        if (deletedMeeting is null)
            return ScheduleResultFactory.Failure(new Exception("Meeting does not exists"));

        return ScheduleResultFactory.Success();
    }
}