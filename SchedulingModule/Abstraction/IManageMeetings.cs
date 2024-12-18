using DataAccess.Model.User;
using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Abstraction;

public interface IManageMeetings
{
    Task<ScheduleResult> GetByMonthYearAndUser(int year, int month, string userGuid);
    Task<ScheduleResult> UnlinkUsers(MeetingRecord record);
    Task<ScheduleResult> LinkUsers(MeetingRecord record, List<string> usersGuids);
    Task<ScheduleResult> UpsertMeeting(MeetingRecord meeting);
    
    Task<ScheduleResult> DeleteMeeting(MeetingRecord meetingToDelete);
}