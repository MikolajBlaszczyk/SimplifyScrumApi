using DataAccess.Abstraction;
using DataAccess.Context;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Accessors;

public class MeetingAccessor(SimplifyAppDbContext dbContext) : IMeetingAccessor
{
    public List<Meeting> GetByMonthAndYearForUserGuid(int month, int year, string userGuid)
    {
        var user = dbContext
            .Users
            .FirstOrDefault(u => u.Id == userGuid);

        if (user == null)
            throw new AccessorException();

        var meetingIds = dbContext
            .TeammateMeetings
            .Where(tm => tm.TeammateGUID == user.Id)
            .Select(tm => tm.MeetingGUID);


        var meetings = dbContext
            .Meetings
            .Where(m => meetingIds.Contains(m.GUID))
            .ToList();


        return meetings;
    }

    public Meeting? GetMeetingById(string identifier) => dbContext
        .Meetings
        .Include(m => m.TeammateMeetings)
        .FirstOrDefault(m => m.GUID == identifier);

    public Meeting? UpsertMeeting(Meeting meeting)
    {
        try
        {
            if(dbContext.Meetings.Any(m => m.GUID == meeting.GUID))
                dbContext.Update(meeting);
            else
                dbContext.Meetings.Add(meeting);
            
            dbContext.SaveChanges();
            
            return meeting;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Meeting? DeleteMeeting(Meeting meeting)
    {
        try
        {
            dbContext.Remove(meeting);
            dbContext.SaveChanges();
            return meeting;
        }
        catch
        {
            return null;
        }
    }

    public TeammateMeetings AddLinkBetweenMeetingAndTeammate(TeammateMeetings link)
    {
        try
        {
            dbContext.TeammateMeetings.Add(link);
            return link;
        }
        catch
        {
            return null;
        }
    }

    public void SaveChanges() => dbContext.SaveChanges();
}