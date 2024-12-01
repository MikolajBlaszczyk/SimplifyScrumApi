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

public class Scheduler(IHttpContextAccessor contextAccessor, IManageMeetings meetingStorage, MeetingGrouper grouper, CalendarArranger arranger) : ISchedule
{
    
    public async Task<ScheduleResult> GetCurrentMonthSchedule(DateTime date, string userGuid)
    {
        try
        {
            var guid = contextAccessor.HttpContext?.User.GetUserGuid();
            var meetingResult = await meetingStorage.GetByMonthYearAndUser(date.Year, date.Month, guid);
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
            var result = await meetingStorage.UpsertMeeting(meeting);
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
            var result = await meetingStorage.DeleteMeeting(meeting);
            await meetingStorage.UnlinkUsers(meeting);
            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}