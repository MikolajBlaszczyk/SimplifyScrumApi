using DataAccess.Abstraction;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using SchedulingModule.Records;
using SchedulingModule.Utils.Extensions;

namespace SchedulingModule.Utils;

public class TeammateLinker(IMeetingAccessor meetingAccessor,  IUserStore<Teammate> userManager)
{
    public bool LinkUsersToMeeting(SimpleMeetingModel meeting, Meeting dbModel)
    {
        if (meeting.UserGuids == null)
            return false;
       
        var teammates = meeting
            .UserGuids
            .Select(guid =>
            {
                var task = userManager.FindByIdAsync(guid, new CancellationToken());
                Task.WaitAll(task);
                return task.Result;
            })
            .ToList();
        
        teammates.ForEach(t =>
        {
            var link = new TeammateMeetings();
            link.CreateLink(t, dbModel);
            meetingAccessor.AddLinkBetweenMeetingAndTeammate(link);
        });
       
        
        return true;
    }
    
}