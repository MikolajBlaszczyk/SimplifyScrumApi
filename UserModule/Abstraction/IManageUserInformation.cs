using UserModule.Models;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Abstraction;

public interface IManageUserInformation
{
    Task<UserInfoResult> GetInfoByUserGUIDAsync(string guid);
    Task<HierarchyResult> GetUsersActiveProjectAsync(string guid);
    Task<UsersInfoResult> GetAllUsersAsync();
    Task<UserInfoResult> UpdateUserInfo(SimpleUserModel updatedUser);
    Task<HierarchyResult> AddTeam(SimpleTeamModel newTeam);
    Task<HierarchyResult> AddUsersToTeam(List<SimpleUserModel> users, SimpleTeamModel team);
    Task<HierarchyResult> GetAllTeamsAsync();
    Task<HierarchyResult> GetTeam(string teamGUID);
    Task<HierarchyResult> GetTeamMemebers(string teamGUID);
    Task<HierarchyResult> UpdateTeamMembers(TeamMembersUpdate teamMembersUpdate);
}