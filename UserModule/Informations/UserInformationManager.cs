using DataAccess.Abstraction;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

    public async Task<HierarchyResult> GetUsersProjectAsync(string guid)
    {
        try
        {
            var user = await userManager.FindByIdAsync(guid);
            
            if (user is null or {TeamGUID: null or ""})
                throw new Exception();

            var project = hierarchyStorage.GetProjectByTeam(user.TeamGUID);
        
            
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
           var allUsers =  userManager
               .Users
               .AsEnumerable()
               .Select( async teammate =>
               {
                   SimpleUserModel user = teammate;
                   user.SystemRole = await roleManager.GetHighestSystemRoleAsync(teammate);
                   return user;
               })
               .Select(task => task.Result)
               .ToList();
           
           return allUsers;
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
            
            var guid = contextAccessor.HttpContext.User.GetUserGuid();
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
            SimpleTeamModel addedTeam = hierarchyStorage.AddTeam(newTeam);

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
            var teams =  hierarchyStorage.GetAllTeams();
            
            
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
            SimpleTeamModel team =  hierarchyStorage.GetTeamByGUID(teamGUID);
            
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
}