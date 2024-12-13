using DataAccess.Enums;
using DataAccess.Model.Meetings;
using DataAccess.Models.Factories;

namespace SchedulingModule.Records;

public record MeetingRecord(
    string GUID,
    string Name,
    string Description,
    string LeaderGuid,
    DateTime Start,
    TimeSpan Duration,
    MeetingType Type,
    List<string>? UserGuids)
{
    
    public static MeetingRecord Create( string guid,
        string name,
        string description,
        string leaderGuid,
        DateTime start,
        TimeSpan duration,
        MeetingType type,
        List<string> users = null)
    {
        
        return new MeetingRecord(
            guid,
            name,
            description,
            leaderGuid,
            start,
            duration,
            type, 
            users
        );
    }
    
    public static  implicit operator Meeting(MeetingRecord? record)
    {
        return MeetingFactory
            .Create(
                record.GUID,
                record.Name,
                record.Description,
                record.LeaderGuid,
                record.Start,
                record.Duration,
                record.Type
            );
    }
    
    public static  implicit operator MeetingRecord(Meeting model)
    {
        var users =model.TeammateMeetings?.Select(tm => tm.TeammateGUID).ToList();
        
        return Create(
            model.GUID,
            model.Name,
            model.Description,
            model.MeetingLeaderGUID,
            model.Start,
            model.Duration,
            model.Type,
            users
        );
    }
}
