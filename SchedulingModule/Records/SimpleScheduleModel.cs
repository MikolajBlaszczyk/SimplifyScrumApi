using SchedulingModule.Enums;

namespace SchedulingModule.Records;

public record SimpleScheduleModel(Month month, List<SimpleDayModel> days);