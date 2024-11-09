using DataAccess.Abstraction;
using SchedulingModule.Abstraction;
using SchedulingModule.Enums;
using SchedulingModule.Models;
using SchedulingModule.Records;
using SchedulingModule.Utils;

namespace SchedulingModule;

public class Scheduler(IMeetingAccessor meetingAccessor, MeetingGrouper grouper, CalendarArranger arranger) : ISchedule
{
    
    public async Task<ScheduleResult> GetScheduleByMonthForCurrentUser(DateTime date, string userGuid)
    {
        var meetings = meetingAccessor.GetByMonthAndYearForUserGuid(date.Month, date.Year, userGuid);
        var groupedMeetings = grouper.GroupMeetingsByDayOfMonth(meetings);
        
        var month = (Month)date.Month;
        var days = arranger.PrepareDaysWithMeetings(date, groupedMeetings);
        var schedule = new SimpleScheduleModel(month, days);

        return schedule;
    }

   
        
}