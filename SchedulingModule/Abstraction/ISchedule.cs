using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Abstraction;

public interface ISchedule
{
    Task<ScheduleResult> GetCurrentMonthSchedule(DateTime date, string userGuid);
    Task<ScheduleResult> ScheduleMeeting(MeetingRecord meeting);
    Task<ScheduleResult> UnScheduleMeeting(MeetingRecord meeting);

}