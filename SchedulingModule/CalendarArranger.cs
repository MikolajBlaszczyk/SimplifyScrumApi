using System.Collections.Immutable;
using SchedulingModule.Records;

namespace SchedulingModule;

public class CalendarArranger
{
    
    private static readonly ImmutableList<MeetingRecord> EmptyList = ImmutableList<MeetingRecord>.Empty;
    
    public List<DayRecord> PrepareDaysWithMeetings(DateTime date, IList<IGrouping<int, MeetingRecord>> groupedMeetings)
    {
        List<DayRecord> days = new();
        var numberOfDays = DateTime.DaysInMonth(date.Year, date.Month);
        
        for (int day = 1; day <= numberOfDays; day++)
        {
            var currenDayMeetings = GetCurrenDayMeetings(groupedMeetings, day);
            var dayFullDate = new DateTime(date.Year, date.Month, day);
            DayRecord currentDay = new(dayFullDate, currenDayMeetings);
            
            days.Add(currentDay);
        }

        return days;
    }
    
    private List<MeetingRecord> GetCurrenDayMeetings(IList<IGrouping<int, MeetingRecord>> groupedMeetings, int i)
    {
        return groupedMeetings.FirstOrDefault(mr => mr.Key == i)?.ToList() ?? EmptyList.ToList();
    }
}