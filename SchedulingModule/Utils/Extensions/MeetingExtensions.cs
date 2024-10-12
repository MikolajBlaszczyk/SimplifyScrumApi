using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using SchedulingModule.Records;

namespace SchedulingModule.Utils.Extensions;

public static class MeetingExtensions
{
    public static void Update(this Meeting meeting, SimpleMeetingModel model)
    {
        meeting.Name = model.Name;
        meeting.MeetingLeaderGUID = model.LeaderIdentifier;
        meeting.Description = model.Description;
        meeting.Duration = model.Duration;
        meeting.Type = model.Type;
        meeting.Start = model.Start;
    }

    public static void CreateLink(this TeammateMeetings link, Teammate teammate, Meeting meeting)
    {
        link.Meeting = meeting;
        link.MeetingGUID = meeting.GUID;
        link.TeammateGUID = teammate.Id;
        link.Teammate = teammate;
    }
}