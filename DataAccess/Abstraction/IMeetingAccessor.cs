using DataAccess.Model.Meetings;

namespace DataAccess.Abstraction;

public interface IMeetingAccessor
{
    List<Meeting> GetByMonthAndYear(int month, int year);
    Meeting? GetMeetingById(string identifier);
    Meeting? UpsertMeeting(Meeting meeting);
    Meeting? DeleteMeeting(Meeting meeting);
}