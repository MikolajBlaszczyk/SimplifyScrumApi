using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;

namespace DataAccess.Abstraction.Storage;

public interface IMeetingStorage
{
    List<Meeting> GetByMonthAndYearForUserGuid(int month, int year, string userGuid);
    Meeting GetMeetingById(string identifier);
    Meeting UpsertMeeting(Meeting meeting);
    Meeting DeleteMeeting(Meeting meeting);
    TeammateMeetings AddUserLink(TeammateMeetings link);
    List<TeammateMeetings> RemoveAllLinks(Meeting meeting);
}