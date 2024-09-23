using SchedulingModule.Records;

namespace SchedulingModule.Models.Factories;

public abstract class ScheduleResultFactory
{
    public static ScheduleResult Success()
    {
        return new ScheduleResult
        {
            IsSuccess = true,
            ScheduleRecord = null,
            IsFailure = false,
            Exception = null
        };
    }

    
    public static ScheduleResult Success(SimpleScheduleModel simpleScheduleModel)
    {
        return new ScheduleResult
        {
            IsSuccess = true,
            ScheduleRecord = simpleScheduleModel,
            IsFailure = false,
            Exception = null
        };
    }

    public static ScheduleResult Failure(Exception ex)
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