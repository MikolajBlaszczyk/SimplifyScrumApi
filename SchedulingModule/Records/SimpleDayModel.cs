using DataAccess.Model.Meetings;
using SchedulingModule.Enums;

namespace SchedulingModule.Records;

public record SimpleDayModel(DateTime date, List<SimpleMeetingModel> meetings);