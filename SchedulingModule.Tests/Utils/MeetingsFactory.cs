using DataAccess.Enums;
using DataAccess.Model.Meetings;
using SchedulingModule.Records;

namespace SchedulingModule.Tests.Utils;

public abstract class MeetingsFactory
{
    public static Meeting CreateMeetingWithDate(string name, int day, int month , int year)
    {
        var start = DateTime.Parse($"{day}.{month}.{year}");
        
        return new Meeting
        {
            Name = name,
            Start = start
        };
    }

    public static MeetingRecord CreateMeetingRecordWithNameAndStart(string name, DateTime start)
    {
        return new MeetingRecord("", name, "", "", start, TimeSpan.Zero, MeetingType.Custom, new());
    }
}