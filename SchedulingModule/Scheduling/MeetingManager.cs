using DataAccess.Abstraction;
using DataAccess.Abstraction.Storage;
using DataAccess.Model.User;
using DataAccess.Models.Factories;
using Microsoft.Extensions.Logging;
using SchedulingModule.Abstraction;
using SchedulingModule.Models;
using SchedulingModule.Records;
using SchedulingModule.Utils;
using SchedulingModule.Utils.Extensions;

namespace SchedulingModule;


public class MeetingManager(IMeetingStorage meetingStorage, UserLinker linker, ILogger<MeetingManager> logger): IManageMeetings
{
    public async Task<ScheduleResult> GetByMonthYearAndUser(int year, int month, string userGuid)
    {
        var meetings = await meetingStorage
            .GetByMonthAndYearForUserGuid(month, year, userGuid);
        
        return meetings.Select(meeting =>
        {
            MeetingRecord record = meeting;
            return record;
        }).ToList();
    }

    public async Task<ScheduleResult> UpsertMeeting(MeetingRecord record)
    {
        MeetingRecord result = await meetingStorage.UpsertMeeting(record);
        return result;
    }

    public async Task<ScheduleResult> GetMeeting(string meetingGuid)
    {
        try
        {
            MeetingRecord result = await meetingStorage.GetMeetingById(meetingGuid);
            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return e;
        }
    }

    public async Task<ScheduleResult> DeleteMeeting(string meetingGuid)
    {
        try
        {
            var meetingToDelete = await meetingStorage.GetMeetingById(meetingGuid);
            await UnlinkUsers(meetingToDelete);
            MeetingRecord result = await meetingStorage.DeleteMeeting(meetingToDelete);
            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return e;
        }
        
    }
    
    public async Task<ScheduleResult> UnlinkUsers(MeetingRecord record)
    {
        await linker.UnlinkAllUsers(record);
        return ScheduleResult.SuccessWithoutData();
    }

    public async Task<ScheduleResult> LinkUsers(MeetingRecord record, List<string> users)
    {
        await linker.LinkUsersToMeeting(record, users);
        return ScheduleResult.SuccessWithoutData();
    }
}