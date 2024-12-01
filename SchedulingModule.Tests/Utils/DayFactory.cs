using SchedulingModule.Records;

namespace SchedulingModule.Tests.Utils;

public abstract class DayFactory
{
    public static Day CreateDayRecord(DateTime date, List<MeetingRecord> meetings)
    {
        return new Day(date, meetings);
    }
}