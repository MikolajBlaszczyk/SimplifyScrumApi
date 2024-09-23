using SchedulingModule.Records;

namespace SchedulingModule.Models;

public class ScheduleResult
{
    public bool IsSuccess { get; init; }
    public bool IsFailure { get; init; }
    public Exception? Exception { get; init;  }
    public SimpleScheduleModel? ScheduleRecord { get; init; }
}