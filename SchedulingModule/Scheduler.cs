using DataAccess.Abstraction;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.User;
using DataAccess.Models.Factories;
using Microsoft.AspNetCore.Identity;
using SchedulingModule.Abstraction;
using SchedulingModule.Enums;
using SchedulingModule.Models;
using SchedulingModule.Models.Factories;
using SchedulingModule.Records;
using SchedulingModule.Utils;
using SchedulingModule.Utils.Extensions;

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

        var result = ScheduleResultFactory.Success(schedule);
        return result;
    }

   
        
}