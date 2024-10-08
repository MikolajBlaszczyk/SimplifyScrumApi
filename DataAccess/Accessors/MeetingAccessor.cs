using DataAccess.Abstraction;
using DataAccess.Context;
using DataAccess.Model.Meetings;

namespace DataAccess.Accessors;

public class MeetingAccessor(SimplifyAppDbContext dbContext) : IMeetingAccessor
{
    public List<Meeting> GetByMonthAndYear(int month, int year) => dbContext
        .Meetings
        .Where(m => m.Start.Month == month && m.Start.Year == year)
        .ToList();

    public Meeting? GetMeetingById(string identifier) => dbContext
        .Meetings
        .FirstOrDefault(m => m.Guid == identifier);

    public Meeting? UpsertMeeting(Meeting meeting)
    {
        try
        {
            if(dbContext.Meetings.Any(m => m.Guid == meeting.Guid))
            {
                dbContext.Update(meeting);
            }
            else
            {
                dbContext.Meetings.Add(meeting);
            }
            
            return meeting;
        }
        catch
        {
            return null;
        }
    }

    public Meeting? DeleteMeeting(Meeting meeting)
    {
        try
        {
            dbContext.Remove(meeting);
            return meeting;
        }
        catch
        {
            return null;
        }
    }
}