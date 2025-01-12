using DataAccess.Abstraction;
using DataAccess.Model.Meetings;
using Microsoft.AspNetCore.Http;
using SchedulingModule.Abstraction;
using SchedulingModule.Enums;
using SchedulingModule.Models;
using SchedulingModule.Records;
using SchedulingModule.Utils;
using SimplifyScrum.Utils;

namespace SchedulingModule;

public class Scheduler(IHttpContextAccessor contextAccessor, IScheduleMeetings meetingStorage, MeetingGrouper grouper, CalendarArranger arranger) : ISchedule
{
    
    public async Task<ScheduleResult> GetScheduleByMonth(DateTime date, string userGuid)
    {
        try
        {
          
            var meetingResult = await meetingStorage.GetByMonthYearAndUser(date.Year, date.Month, userGuid);
            var meetings = (meetingResult.Data as List<MeetingRecord>).Select(meeting =>
            {
                Meeting record = meeting;
                return record;
            }).ToList();
            var groupedMeetings = grouper.GroupMeetingsByDayOfMonth(meetings);
        
            var month = (Month)date.Month;
            var days = arranger.PrepareDaysWithMeetings(date, groupedMeetings);
            var schedule = new Schedule(month, days);

            return schedule;
        }
        catch (Exception e)
        {
            return e;
        }
    }

   

    public async Task<ScheduleResult> ScheduleMeeting(MeetingRecord meeting)
    {
        try
        {
            var result = await meetingStorage.AddMeeting(meeting);
            await meetingStorage.LinkUsers(meeting, meeting.UserGuids);
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ScheduleResult> UnScheduleMeeting(MeetingRecord meeting)
    {
        try
        {
            var result = await meetingStorage.DeleteMeeting(meeting.GUID);
            await meetingStorage.UnlinkUsers(meeting);
            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}