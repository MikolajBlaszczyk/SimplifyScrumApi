using DataAccess.Model.Meetings;

namespace DataAccess.Abstraction;

public interface IMeetingAccessor
{
    List<Meeting> GetByMonthAndYearForUserName(int month, int year, string user);
    Meeting? GetMeetingById(string identifier);
    Meeting? UpsertMeeting(Meeting meeting);
    Meeting? DeleteMeeting(Meeting meeting);
}