using DataAccess.Model.Meetings;
using SchedulingModule.Records;

namespace SchedulingModule.Utils;

public class ModelConverter
{
    public MeetingRecord ConvertIntoRecord(Meeting meeting)
    {
        var usersGuids = meeting
            .TeammateMeetings?
            .Select(tm => tm.TeammateGuid)
            .ToList();
        
        return new MeetingRecord(
            meeting.Guid, 
            meeting.Name,
            meeting.Description,
            meeting.MeetingLeaderGuid,
            meeting.Start, 
            meeting.Duration,
            meeting.Type,
            usersGuids);
    }
}