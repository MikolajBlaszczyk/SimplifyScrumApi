using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using SchedulingModule.Records;

namespace SchedulingModule.Utils.Extensions;

public static class MeetingExtensions
{
    public static void Update(this Meeting meeting, MeetingRecord record)
    {
        meeting.Name = record.Name;
        meeting.MeetingLeaderGUID = record.LeaderGuid;
        meeting.Description = record.Description;
        meeting.Duration = record.Duration;
        meeting.Type = record.Type;
        meeting.Start = record.Start;
    }

    public static void CreateLink(this TeammateMeetings link, string userGuid, string meetingGuid)
    {
        link.TeammateGUID = userGuid;
        link.MeetingGUID = meetingGuid;
    }
}