using DataAccess.Abstraction;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimplifyScrum.Utils;
using UserModule.Abstraction;
using UserModule.Models;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Informations;

public class UserInformationManager(
    IHttpContextAccessor contextAccessor,
    UserManager<Teammate> userManager,
    IRoleManager roleManager,
    IUserHierarchyStorage hierarchyStorage): IManageUserInformation
{
    public async Task<UserInfoResult> GetInfoByUserGUIDAsync(string guid)
    {
        try
        {
            var teammate = await userManager.FindByIdAsync(guid);
            var role = await roleManager.GetHighestSystemRoleAsync(teammate);

            SimpleUserModel appUser = teammate;
            appUser.SystemRole = role;
            
            return appUser;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<HierarchyResult> GetUsersActiveProjectAsync(string guid)
    {
        try
        {
            var user = await userManager.FindByIdAsync(guid);
            
            if (user is null)
                throw new Exception();

            var project = await hierarchyStorage.GetActiveProjectByTeam(user.TeamGUID);
        
            
            return project;

        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<UsersInfoResult> GetAllUsersAsync()
    {
        try
        {
           var allUsers = await userManager
               .Users
               .ToListAsync();

           var users = allUsers
               .Select(async teammate =>
               {
                   SimpleUserModel user = teammate;
                   user.SystemRole = await roleManager.GetHighestSystemRoleAsync(teammate);
                   return user;
               })
               .Select(task => task.Result);
           
           return users.ToList();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<UserInfoResult> UpdateUserInfo(SimpleUserModel updatedUser)
    {
        try
        {
            var updatedTeammate = updatedUser;

            var guid = updatedUser.Id;
            var original =  await userManager.FindByIdAsync(guid);
            
            original.Update(updatedTeammate);
            var updateResult = await userManager.UpdateAsync(original);
            if (updateResult.Succeeded)
                return updatedUser;
         
            throw new Exception();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<HierarchyResult> AddTeam(SimpleTeamModel newTeamModel)
    {
        try
        {
            //TODO: Validation here
            Team newTeam = newTeamModel;
            SimpleTeamModel addedTeam = await hierarchyStorage.AddTeam(newTeam);

            return addedTeam;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<HierarchyResult> AddUsersToTeam(List<SimpleUserModel> users, SimpleTeamModel team)
    {
        try
        {
            //TODO: Validation here
            foreach (var user in users)
            {
                var teammate = await userManager.FindByIdAsync(user.Id);
                teammate.TeamGUID = team.GUID;
                await userManager.UpdateAsync(teammate);
            }

            return HierarchyResult.SuccessWithoutData();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<HierarchyResult> GetAllTeamsAsync()
    {
        try
        {
            var result = new List<SimpleTeamModel>();
            var teams = await hierarchyStorage.GetAllTeams();
            
            
            foreach (var team in teams)
            {
                result.Add(team);
            }
            
            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<HierarchyResult> GetTeam(string teamGUID)
    {
        try
        {
            SimpleTeamModel team = await hierarchyStorage.GetTeamByGUID(teamGUID);
            
            return team;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<HierarchyResult> GetTeamMemebers(string teamGUID)
    {
        try
        {
            List<SimpleUserModel> result = new List<SimpleUserModel>();
            var members = userManager.Users.Where(u => u.TeamGUID == teamGUID).ToList();

            foreach (var member in members)
            {
                SimpleUserModel user = member;
                result.Add(user);
            }

            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<HierarchyResult> UpdateTeamMembers(TeamMembersUpdate teamMembersUpdate)
    {
        try
        {
            List<SimpleUserModel> updatedTeamMembers = new List<SimpleUserModel>();
            
            foreach (var id in teamMembersUpdate.userIDs)
            {
                var original =  await userManager.FindByIdAsync(id);
                original.TeamGUID = teamMembersUpdate.teamGUID;
                await userManager.UpdateAsync(original);
                updatedTeamMembers.Add(original);
            }
            
            var currentMembers = await userManager.Users
                .Where(u => u.TeamGUID == teamMembersUpdate.teamGUID)
                .ToListAsync();
            
            foreach (var member in currentMembers)
            {
                if(updatedTeamMembers.Any(u => u.Id == member.Id))
                    continue;
                
                var original =  await userManager.FindByIdAsync(member.Id);
                original.TeamGUID = null;
                await userManager.UpdateAsync(original);
                updatedTeamMembers.Add(original);
            }

            return updatedTeamMembers;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}