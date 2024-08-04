using DataAccess.Model.Meetings;
using SchedulingModule.Enums;

namespace SchedulingModule.Records;

public record DayRecord(DateTime date, List<MeetingRecord> meetings);