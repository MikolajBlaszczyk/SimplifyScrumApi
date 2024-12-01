using DataAccess.Model.Meetings;
using SchedulingModule.Enums;

namespace SchedulingModule.Records;

public record Day(DateTime date, List<MeetingRecord> meetings);