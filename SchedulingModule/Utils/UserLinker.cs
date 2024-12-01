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
    public void LinkUsersToMeeting(MeetingRecord meeting, List<string> guids)
    {
        foreach (var guid in guids)
        {
            var link = new TeammateMeetings();
            link.CreateLink(guid, meeting.GUID);
            meetingStorage.AddUserLink(link);
        }
    }

    public async Task UnlinkAllUsers(MeetingRecord record)
    {
        meetingStorage.RemoveAllLinks(record);
    }
}