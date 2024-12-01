using DataAccess.Model.Meetings;
using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Tests.Models;

public record ScheduleTestResult(List<MeetingRecord> RawMeetings, ScheduleResult Expected);