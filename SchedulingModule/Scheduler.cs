using DataAccess.Abstraction;
using DataAccess.Models.Factories;
using SchedulingModule.Abstraction;
using SchedulingModule.Enums;
using SchedulingModule.Models;
using SchedulingModule.Models.Factories;
using SchedulingModule.Records;
using SchedulingModule.Utils;
using SchedulingModule.Utils.Extensions;

namespace SchedulingModule;

public class Scheduler(IMeetingAccessor accessor, MeetingGrouper grouper, CalendarArranger arranger) : ISchedule
{
    
    public async Task<ScheduleResult> GetScheduleByMonthForCurrentUser(DateTime date, string userGuid)
    {
        var meetings = accessor.GetByMonthAndYearForUserGuid(date.Month, date.Year, userGuid);
        var groupedMeetings = grouper.GroupMeetingsByDayOfMonth(meetings);
        
        var month = (Month)date.Month;
        var days = arranger.PrepareDaysWithMeetings(date, groupedMeetings);
        var schedule = new ScheduleRecord(month, days);

        var result = ScheduleResultFactory.CreateSuccessResultWithSchedule(schedule);
        return result;
    }

    public async Task<ScheduleResult> UpsertMeeting(MeetingRecord meetingToUpsert)
    {
        var meeting = accessor.GetMeetingById(meetingToUpsert.Identifier);
        
        if(meeting is not null)
        {
            meeting.Update(meetingToUpsert);
        }
        else
        {
            meeting = MeetingFactory.CreateMeetingWithGuid(
                meetingToUpsert.Identifier,
                meetingToUpsert.Name,
                meetingToUpsert.Description,
                meetingToUpsert.LeaderIdentifier,
                meetingToUpsert.Start,
                meetingToUpsert.Duration,
                meetingToUpsert.Type );
        }

        var result = accessor.UpsertMeeting(meeting);

        if (result is null)
            return ScheduleResultFactory.CreateFailResult(new Exception("Could not upsert the meeting"));

        return ScheduleResultFactory.CreateSuccessResult();
    }

    public async Task<ScheduleResult> DeleteMeeting(MeetingRecord meetingToDelete)
    {
        var meeting = accessor.GetMeetingById(meetingToDelete.Identifier);

        if (meeting is null)
            return ScheduleResultFactory.CreateFailResult(new Exception("Meeting does not exists"));
        
        var deletedMeeting = accessor.DeleteMeeting(meeting);
        
        if (deletedMeeting is null)
            return ScheduleResultFactory.CreateFailResult(new Exception("Meeting does not exists"));

        return ScheduleResultFactory.CreateSuccessResult();
    }
        
}