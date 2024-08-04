using SchedulingModule.Records;

namespace SchedulingModule.Models.Factories;

public abstract class ScheduleResultFactory
{
    public static ScheduleResult CreateSuccessResult()
    {
        return new ScheduleResult
        {
            IsSuccess = true,
            ScheduleRecord = null,
            IsFailure = false,
            Exception = null
        };
    }

    
    public static ScheduleResult CreateSuccessResultWithSchedule(ScheduleRecord scheduleRecord)
    {
        return new ScheduleResult
        {
            IsSuccess = true,
            ScheduleRecord = scheduleRecord,
            IsFailure = false,
            Exception = null
        };
    }

    public static ScheduleResult CreateFailResult(Exception ex)
    {
        return new ScheduleResult()
        {
            IsFailure = true,
            Exception = ex,
            IsSuccess = false,
            ScheduleRecord = null
        };
    }
}