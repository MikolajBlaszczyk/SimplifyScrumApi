using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;

namespace DataAccess.Models.Factories;

public class MeetingFactory
{
    public static Meeting CreateMeetingWithGuid(string guid, string name, string description, string leaderGuid,  DateTime start, TimeSpan duration,MeetingType type)
    {
        if (Guid.TryParse(guid, out _) == false)
            guid = Guid.NewGuid().ToString();
        
        return new Meeting
        {
            Guid = guid,
            Name = name,
            Description = description,
            Start = start,
            Duration = duration,
            MeetingLeaderGuid = leaderGuid,
            Type = type
        };
    }
    
    public static Meeting CreateMeetingWithGuid(string guid, string name, string description, string leaderGuid,  DateTime start, TimeSpan duration,MeetingType type, List<TeammateMeetings> links)
    {
        if (Guid.TryParse(guid, out _) == false)
            guid = Guid.NewGuid().ToString();
        
        return new Meeting
        {
            Guid = guid,
            Name = name,
            Description = description,
            Start = start,
            Duration = duration,
            MeetingLeaderGuid = leaderGuid,
            Type = type,
            TeammateMeetings = links
        };
    }
}