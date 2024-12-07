using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;

namespace DataAccess.Abstraction.Storage;

public interface IMeetingStorage
{
    Task<List<Meeting>> GetByMonthAndYearForUserGuid(int month, int year, string userGuid);
    Task<Meeting> GetMeetingById(string identifier);
    Task<Meeting> UpsertMeeting(Meeting meeting);
    Task<Meeting> DeleteMeeting(Meeting meeting);
    Task<TeammateMeetings> AddUserLink(TeammateMeetings link);
    Task<List<TeammateMeetings>> RemoveAllLinks(Meeting meeting);
}