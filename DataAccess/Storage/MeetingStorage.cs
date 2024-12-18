using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using DataAccess.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class MeetingStorage(UserManager<Teammate> userManager, ICreateAccessors factory, ILogger<MeetingStorage> logger) : IMeetingStorage
{
    
    
    
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
        var meetingAccessor = factory.Create<Meeting>();

        var meetingIds = await dbContext
            .TeammateMeetings
            .Where(tm => tm.TeammateGUID == user.Id)
            .Select(tm => tm.MeetingGUID as object)
            .ToListAsync();
        
        var meetings = await meetingAccessor.GetAllByPKs(meetingIds);
        if (meetings is null)
        {
            logger.LogError("Could not retrieved meetings");
            throw new AccessorException("Could not retrieved meetings");
        }
        
        return meetings;
    }
    
    public async Task<Meeting> GetMeetingById(string identifier)
    {
        var meetingAccessor =factory.Create<Meeting>();
        
        var meeting = await meetingAccessor.GetByPK(identifier);
        if (meeting is null)
        {
            logger.LogError("Could not retrieved meeting");
            throw new AccessorException("Could not retrieved meeting");
        }

        return meeting;
    } 

    public async Task<Meeting> UpsertMeeting(Meeting meeting)
    {
        Meeting? result;
        var meetingAccessor = factory.Create<Meeting>();

        if (await meetingAccessor.GetByPK(meeting.GUID) is not null)
        {
            result = await meetingAccessor.Update(meeting);
        }
        else
        {
            result = await meetingAccessor.Add(meeting);
        }

        if (result is null)
        {
            logger.LogError("Could not upsert meeting");
            throw new AccessorException("Could not upsert meeting");
        }
        
        return result;
    }
    
    public async Task<Meeting> DeleteMeeting(Meeting meeting)
    {
        var meetingAccessor = factory.Create<Meeting>();

        var result = await meetingAccessor.Delete(meeting);
        if (result is null)
        {
            logger.LogError("Could not delete meeting");
            throw new AccessorException("Could not delete meeting");
        }
   
        return result;
    }
    
    public async Task<TeammateMeetings> AddUserLink(TeammateMeetings link)
    {
        var dbContext = factory.DbContext;
        
        try
        {
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
            var links = await dbContext.TeammateMeetings.ToListAsync();
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