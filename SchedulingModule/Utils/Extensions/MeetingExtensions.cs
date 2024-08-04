using DataAccess.Model.Meetings;
using SchedulingModule.Records;

namespace SchedulingModule.Utils.Extensions;

public static class MeetingExtensions
{
    public static void Update(this Meeting meeting, MeetingRecord record)
    {
        meeting.Name = record.Name;
        meeting.MeetingLeaderGuid = record.LeaderIdentifier;
        meeting.Description = record.Description;
        meeting.Duration = record.Duration;
        meeting.Type = record.Type;
        meeting.Start = record.Start;
    }
}