using DataAccess.Enums;

namespace SchedulingModule.Records;

public record MeetingRecord(string Identifier, string Name, string Description, string LeaderIdentifier, DateTime Start, TimeSpan Duration, MeetingType Type);
