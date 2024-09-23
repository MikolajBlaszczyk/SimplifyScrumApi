using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Abstraction;

public interface ISchedule
{
    Task<ScheduleResult> GetScheduleByMonth(DateTime date, string name);

    Task<ScheduleResult> UpsertMeeting(MeetingRecord meetingToUpsert);
    
    Task<ScheduleResult> DeleteMeeting(MeetingRecord meetingToDelete);
}