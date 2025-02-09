using DataAccess.Abstraction.Storage;
using DataAccess.Enums.Notification;
using DataAccess.Models.Factories;
using Microsoft.Extensions.Logging;
using SchedulingModule.Abstraction;
using SchedulingModule.Models;
using SchedulingModule.Records;
using SchedulingModule.Utils;

namespace SchedulingModule.Scheduling;


public class MeetingSchedulerManager(IMeetingStorage meetingStorage, INotificationStorage notificationStorage, UserLinker linker, ILogger<MeetingSchedulerManager> logger): IScheduleMeetings
{
    public async Task<ScheduleResult> GetAllMeetings()
    {
        try
        {
            var meetings = await meetingStorage.GetAllMeetings();
            List<MeetingRecord> results = meetings.Select(m =>
            {
                MeetingRecord record = m;
                return record;
            }).ToList();
            return results;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return e;
        }
    }

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

    public async Task<ScheduleResult> AddMeeting(MeetingRecord meeting)
    {
        try
        {
            MeetingRecord result = await meetingStorage.AddMeeting(meeting);

            
            
            if (meeting.UserGuids != null)
            {
                if(meeting.UserGuids.Count == 0)
                    meeting.UserGuids.Add(meeting.LeaderGuid);
                
                var notification = NotificationFactory.Create(
                    result.GUID,
                    NotificationItem.Meeting,
                    NotificationType.Reminder, 
                    30, 
                    false,
                    meeting.UserGuids);
                await notificationStorage.AddAsync(notification);
            }
            
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<ScheduleResult> UpdateMeeting(MeetingRecord meeting)
    {
        try
        {
            MeetingRecord result = await meetingStorage.UpdateMeeting(meeting);

            var oldNotification = await  notificationStorage.GetByNotificationSourceGUIDAsync(meeting.GUID);
            if (oldNotification.Any())
            {
                await notificationStorage.DeleteAsync(oldNotification.FirstOrDefault().ID);
            }
            
            if (meeting.UserGuids != null)
            {
                var notification = NotificationFactory.Create(
                    result.GUID,
                    NotificationItem.Meeting,
                    NotificationType.Reminder, 
                    30, 
                    false,
                    meeting.UserGuids);
                await notificationStorage.AddAsync(notification);
            }
            
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ScheduleResult> GetMeeting(string meetingGuid)
    {
        try
        {
            MeetingRecord result = await meetingStorage.GetMeetingById(meetingGuid);
            var links =  await linker.GetLinksForMeeting(meetingGuid);
            result.UserGuids.AddRange(links.Select(l => l.TeammateGUID).ToList());
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