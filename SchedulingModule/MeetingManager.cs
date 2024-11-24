using DataAccess.Abstraction;
using DataAccess.Models.Factories;
using SchedulingModule.Abstraction;
using SchedulingModule.Models;
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
            meeting = MeetingFactory.Create(
                simpleMeeting.Identifier,
                simpleMeeting.Name,
                simpleMeeting.Description,
                simpleMeeting.LeaderIdentifier,
                simpleMeeting.Start.ToLocalTime(),
                simpleMeeting.Duration,
                simpleMeeting.Type );
        }

        var usersLinked = linker.LinkUsersToMeeting(simpleMeeting, meeting);
        if (usersLinked == false)
            return new Exception("Cannot link users");
        
        var result = meetingAccessor.UpsertMeeting(meeting);

        if (result is null)
            return new Exception("Could not upsert the meeting");

        return ScheduleResult.SuccessWithoutData();
    }

    public async Task<ScheduleResult> DeleteMeeting(SimpleMeetingModel simpleMeetingToDelete)
    {
        var meeting = meetingAccessor.GetMeetingById(simpleMeetingToDelete.Identifier);

        if (meeting is null)
            return new Exception("Meeting does not exists");

        var usersUnlinked = linker.UnlinkAllUsers(meeting);
        if (usersUnlinked == false)
            return new Exception("Could not all unlink users");
        
        var deletedMeeting = meetingAccessor.DeleteMeeting(meeting);
        
        if (deletedMeeting is null)
            return new Exception("Meeting does not exists");

        return ScheduleResult.SuccessWithoutData();
    }
}