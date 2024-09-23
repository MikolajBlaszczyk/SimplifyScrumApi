using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Abstraction;

public interface ISchedule
{
    Task<ScheduleResult> GetScheduleByMonthForCurrentUser(DateTime date, string userGuid);

    Task<ScheduleResult> UpsertMeeting(MeetingRecord meetingToUpsert);
    
    Task<ScheduleResult> DeleteMeeting(MeetingRecord meetingToDelete);
}