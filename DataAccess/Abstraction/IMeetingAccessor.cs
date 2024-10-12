using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;

namespace DataAccess.Abstraction;

public interface IMeetingAccessor
{
    List<Meeting> GetByMonthAndYearForUserGuid(int month, int year, string userGuid);
    Meeting? GetMeetingById(string identifier);
    Meeting? UpsertMeeting(Meeting meeting);
    Meeting? DeleteMeeting(Meeting meeting);
    TeammateMeetings? AddLinkBetweenMeetingAndTeammate(TeammateMeetings link);
    void SaveChanges();
}