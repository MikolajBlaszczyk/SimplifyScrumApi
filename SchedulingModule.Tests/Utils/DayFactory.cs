using SchedulingModule.Records;

namespace SchedulingModule.Tests.Utils;

public abstract class DayFactory
{
    public static SimpleDayModel CreateDayRecord(DateTime date, List<SimpleMeetingModel> meetings)
    {
        return new SimpleDayModel(date, meetings);
    }
}