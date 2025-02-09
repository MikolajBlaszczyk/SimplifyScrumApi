using DataAccess.Abstraction;
using DataAccess.Abstraction.Storage;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using SchedulingModule.Records;
using SchedulingModule.Utils.Extensions;

namespace SchedulingModule.Utils;

public class UserLinker(IMeetingStorage meetingStorage,  IUserStore<Teammate> userManager)
{
    public async Task LinkUsersToMeeting(MeetingRecord meeting, List<string> guids)
    {
        foreach (var guid in guids)
        {
            var link = new TeammateMeetings();
            link.CreateLink(guid, meeting.GUID);
            await meetingStorage.AddUserLink(link);
        }
    }

    public async Task<List<TeammateMeetings>> GetLinksForMeeting(string guid)
    {
        return await meetingStorage.GetLinksByMeetingGuid(guid);
    }
    
    public async Task UnlinkAllUsers(MeetingRecord record)
    {
        await meetingStorage.RemoveAllLinks(record);
    }
}