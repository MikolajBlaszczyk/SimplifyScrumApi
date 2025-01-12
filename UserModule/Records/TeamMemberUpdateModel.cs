namespace UserModule.Records;

public record TeamMembersUpdate(List<string> userIDs, string teamGUID);