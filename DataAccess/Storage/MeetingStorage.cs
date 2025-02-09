using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using DataAccess.Models.Notifications;
using DataAccess.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class MeetingStorage(UserManager<Teammate> userManager, ICreateAccessors factory, ILogger<MeetingStorage> logger) : IMeetingStorage
{
    private readonly IAccessor<Meeting> meetingAccessor = factory.Create<Meeting>();
    
    public async Task<List<Meeting>> GetAllMeetings()
    {
        return await meetingAccessor.GetAll();
    }

    public async Task<List<Meeting>> GetByMonthAndYearForUserGuid(int month, int year, string userGuid)
    { 
        var user = await userManager
            .Users
            .FirstOrDefaultAsync(u => u.Id == userGuid);

        if (user == null)
        {
            logger.LogError($"User with {userGuid} does not exsists");
            throw new AccessorException("User does not exists");
        }
        
        var dbContext = factory.DbContext;

        var teammateMeetings =  await dbContext
            .TeammateMeetings
            .Where(tm => tm.TeammateGUID == user.Id)
            .ToListAsync();
        var meetingIds = teammateMeetings.Select(tm => tm.MeetingGUID as object).ToList();
        
        var meetings = await meetingAccessor.GetAllByPKs( meetingIds);
        meetings = meetings?.Where(m => m.Start.Year == year && m.Start.Month == month).ToList();
        if (meetings is null)
        {
            logger.LogWarning("Could not retrieved meetings");
            return new List<Meeting>();
        }
        
        return meetings;
    }
    
    public async Task<Meeting> GetMeetingById(string identifier)
    {
        var meeting = await meetingAccessor.GetByPK(identifier);
        if (meeting is null)
        {
            logger.LogError("Could not retrieved meeting");
            throw new AccessorException("Could not retrieved meeting");
        }

        return meeting;
    }

    public async Task<Meeting> AddMeeting(Meeting meeting)
    {
        Meeting? result = await meetingAccessor.Add(meeting);
        if (result is null)
        {
            logger.LogError("Could not add meeting");
            throw new AccessorException("Could not upsert meeting");
        }
        
        return result;
    }

    public async Task<Meeting> UpdateMeeting(Meeting meeting)
    {
        Meeting? result = await meetingAccessor.Update(meeting);
        if (result is null)
        {
            logger.LogError("Could not update meeting");
            throw new AccessorException("Could not upsert meeting");
        }
        
        return result;
    }
    
    public async Task<Meeting> DeleteMeeting(Meeting meeting)
    {

        var result = await meetingAccessor.Delete(meeting);
        if (result is null)
        {
            logger.LogError("Could not delete meeting");
            throw new AccessorException("Could not delete meeting");
        }
   
        return result;
    }

    public async Task<List<TeammateMeetings>> GetLinksByMeetingGuid(string meetingGuid)
    {
        var dbContext = factory.DbContext;

        try
        {
            if(dbContext.TeammateMeetings.Any(tm => tm.MeetingGUID == meetingGuid) == false)
            {
                logger.LogWarning("Link already exists");
                return new List<TeammateMeetings>();
            }

            var teammateMeetings = await dbContext.TeammateMeetings.Where(tm => tm.MeetingGUID == meetingGuid).ToListAsync();
            return teammateMeetings;
        }
        catch(Exception ex)
        {
            logger.LogError("Could not add a link");
            throw new AccessorException("Could not add a link");
        }
    }

    public async Task<TeammateMeetings> AddUserLink(TeammateMeetings link)
    {
        var dbContext = factory.DbContext;
        
        try
        {
            if(dbContext.TeammateMeetings.Any(tm => tm.MeetingGUID == link.MeetingGUID && tm.TeammateGUID == link.TeammateGUID))
            {
                logger.LogWarning("Link already exists");
                return link;
            }
            await dbContext.TeammateMeetings.AddAsync(link);
            await dbContext.SaveChangesAsync();
            return link;
        }
        catch(Exception ex)
        {
            logger.LogError("Could not add a link");
            throw new AccessorException("Could not add a link");
        }
    }

    public async Task<List<TeammateMeetings>> RemoveAllLinks(Meeting meeting)
    {
        var dbContext = factory.DbContext;
        
        try
        {
            var links = await dbContext.TeammateMeetings
                .Where(teammateMeetings => teammateMeetings.MeetingGUID == meeting.GUID)
                .ToListAsync();
            dbContext.TeammateMeetings.RemoveRange(links);
            await dbContext.SaveChangesAsync();
            return links;
        }
        catch(Exception ex)
        {
            logger.LogError("Could not remove links");
            throw new AccessorException("Could not remove links");
        }
    }
}