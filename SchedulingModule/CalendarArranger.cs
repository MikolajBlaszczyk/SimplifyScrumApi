using System.Collections.Immutable;
using SchedulingModule.Records;

namespace SchedulingModule;

public class CalendarArranger
{
    
    private static readonly ImmutableList<SimpleMeetingModel> EmptyList = ImmutableList<SimpleMeetingModel>.Empty;
    
    public List<SimpleDayModel> PrepareDaysWithMeetings(DateTime date, IList<IGrouping<int, SimpleMeetingModel>> groupedMeetings)
    {
        List<SimpleDayModel> days = new();
        var numberOfDays = DateTime.DaysInMonth(date.Year, date.Month);
        
        for (int day = 1; day <= numberOfDays; day++)
        {
            var currenDayMeetings = GetCurrenDayMeetings(groupedMeetings, day);
            var dayFullDate = new DateTime(date.Year, date.Month, day);
            SimpleDayModel currentSimpleDay = new(dayFullDate, currenDayMeetings);
            
            days.Add(currentSimpleDay);
        }

        return days;
    }
    
    private List<SimpleMeetingModel> GetCurrenDayMeetings(IList<IGrouping<int, SimpleMeetingModel>> groupedMeetings, int i)
    {
        return groupedMeetings.FirstOrDefault(mr => mr.Key == i)?.ToList() ?? EmptyList.ToList();
    }
}