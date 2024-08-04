using SchedulingModule.Enums;

namespace SchedulingModule.Records;

public record ScheduleRecord(Month month, List<DayRecord> days);