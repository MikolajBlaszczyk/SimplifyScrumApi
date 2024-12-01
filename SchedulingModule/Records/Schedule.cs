using SchedulingModule.Enums;

namespace SchedulingModule.Records;

public record Schedule(Month month, List<Day> days);