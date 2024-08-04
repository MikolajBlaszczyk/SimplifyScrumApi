using SchedulingModule.Records;

namespace SchedulingModule.Tests.Utils;

public abstract class DayFactory
{
    public static DayRecord CreateDayRecord(DateTime date, List<MeetingRecord> meetings)
    {
        return new DayRecord(date, meetings);
    }
}