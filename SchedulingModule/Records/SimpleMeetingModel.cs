using DataAccess.Enums;

namespace SchedulingModule.Records;

public record SimpleMeetingModel(string Identifier, string Name, string Description, string LeaderIdentifier, DateTime Start, TimeSpan Duration, MeetingType Type, List<string>? UserGuids);
