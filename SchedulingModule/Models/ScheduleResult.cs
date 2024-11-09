using SchedulingModule.Records;

namespace SchedulingModule.Models;

public class ScheduleResult
{
    public ScheduleResult()
    {
        IsSuccess = true;
        ScheduleRecord = null;
        IsFailure = false;
        Exception = null;
    }
    public ScheduleResult(SimpleScheduleModel simpleScheduleModel)
    {
        IsSuccess = true;
        ScheduleRecord = simpleScheduleModel;
        IsFailure = false;
        Exception = null;
    }
    public ScheduleResult(Exception ex)
    {
        IsFailure = true;
        Exception = ex;
        IsSuccess = false;
        ScheduleRecord = null;
    }
    
    public bool IsSuccess { get; init; }
    public bool IsFailure { get; init; }
    public Exception? Exception { get; init;  }
    public SimpleScheduleModel? ScheduleRecord { get; init; }
    
    public static ScheduleResult SuccessWithoutData() => new();
    public static implicit operator ScheduleResult(SimpleScheduleModel schedule) => new (schedule);
    public static implicit operator ScheduleResult(Exception ex) => new(ex);
}