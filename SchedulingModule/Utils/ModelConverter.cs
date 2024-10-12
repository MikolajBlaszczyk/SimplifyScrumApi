using DataAccess.Model.Meetings;
using SchedulingModule.Records;

namespace SchedulingModule.Utils;

public class ModelConverter
{
    public SimpleMeetingModel ConvertIntoRecord(Meeting meeting)
    {
        var usersGuids = meeting
            .TeammateMeetings?
            .Select(tm => tm.TeammateGUID)
            .ToList();
        
        return new SimpleMeetingModel(
            meeting.GUID, 
            meeting.Name,
            meeting.Description,
            meeting.MeetingLeaderGUID,
            meeting.Start, 
            meeting.Duration,
            meeting.Type,
            usersGuids);
    }
}