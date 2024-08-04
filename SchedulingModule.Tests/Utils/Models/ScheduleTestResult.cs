using DataAccess.Model.Meetings;
using SchedulingModule.Models;

namespace SchedulingModule.Tests.Models;

public record ScheduleTestResult(List<Meeting> RawMeetings, ScheduleResult Expected);