using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Abstraction;

public interface ISchedule
{
    Task<ScheduleResult> GetScheduleByMonthForCurrentUser(DateTime date, string userGuid);
    
}