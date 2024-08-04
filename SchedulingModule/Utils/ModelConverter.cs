using DataAccess.Model.Meetings;
using SchedulingModule.Records;

namespace SchedulingModule.Utils;

public class ModelConverter
{
    public MeetingRecord ConvertIntoRecord(Meeting meeting)
    {
        return new MeetingRecord(
            meeting.Guid, 
            meeting.Name,
            meeting.Description,
            meeting.MeetingLeaderGuid,
            meeting.Start, 
            meeting.Duration,
            meeting.Type);
    }
}