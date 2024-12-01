using SchedulingModule.Records;
using UserModule.Security.Models;

namespace SchedulingModule.Models;

public class ScheduleResult : BaseResult
{
    
    public ScheduleResult() {}
    public ScheduleResult(dynamic data) { Data = data; }
    public ScheduleResult(Exception ex) : base(ex) { }
    
    public static ScheduleResult SuccessWithoutData() => new();

    public static implicit operator ScheduleResult(MeetingRecord meeting) => new(meeting);
    public static implicit operator ScheduleResult(List<MeetingRecord> meetings) => new (meetings);
    public static implicit operator ScheduleResult(Schedule schedule) => new (schedule);
    public static implicit operator ScheduleResult(Exception ex) => new(ex);
}