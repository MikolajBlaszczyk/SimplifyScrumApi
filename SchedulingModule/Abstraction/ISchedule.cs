using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Abstraction;

public interface ISchedule
{
    Task<ScheduleResult> GetScheduleByMonth(DateTime date, string userGuid);
    Task<ScheduleResult> ScheduleMeeting(MeetingRecord meeting);
    Task<ScheduleResult> UnScheduleMeeting(MeetingRecord meeting);

}