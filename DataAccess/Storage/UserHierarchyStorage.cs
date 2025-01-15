using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Context;
using DataAccess.Model.User;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class UserHierarchyStorage(ICreateAccessors factory, ILogger<UserHierarchyStorage> logger) : IUserHierarchyStorage
{
    public async Task<Team> GetTeam(string teamGUID)
    {
        var teamAccessor = factory.Create<Team>();

        var team = await teamAccessor.GetByPK(teamGUID);

        if (team is null)
        {
            logger.LogError($"Team with {teamGUID} does not exists");
            throw new AccessorException("User does not exists");        
        }
        
        return team;
    }

    public async Task<Project> GetActiveProjectByTeam(string teamGUID)
    {
        if (string.IsNullOrEmpty(teamGUID))
            return null;
        
        var dbContext = factory.DbContext;
        
        var project = await dbContext
            .Projects
            .FirstOrDefaultAsync(p => p.TeamGUID == teamGUID && p.IsActive == true);

        if (project is null)
        {
            logger.LogWarning($"Project with assigned Team {teamGUID} does not exists");
            return null;
        }

        return project;
    }

    public async Task<Team> AddTeam(Team newTeam)
    {
        var teamAccessor = factory.Create<Team>();
        
        if (string.IsNullOrEmpty(newTeam.GUID))
            newTeam.GUID = Guid.NewGuid().ToString();

        var team =  await teamAccessor.Add(newTeam);

        if (team is null)
        {
            logger.LogError("Team was not properly added");
            throw new AccessorException("Team was not properly added"); 
        }
     
        return newTeam;
    }

    public async Task<List<Team>> GetAllTeams()
    {
        var teamAccessor = factory.Create<Team>();
        var teams = await teamAccessor.GetAll();
        
        if (teams is null)
        {
            logger.LogError("Teams was not retrieved");
            throw new AccessorException("Teams was not retrieved"); 
        }

        return teams;
    }

    public async Task<Team> GetTeamByGUID(string teamGUID)
    {
        var teamAccessor = factory.Create<Team>();
        
        var team = await teamAccessor.GetByPK(teamGUID);
        if (team is null)
        {
            logger.LogError($"Team with {teamGUID} is not in the database");
            throw new AccessorException($"Team with {teamGUID} is not in the database"); 
        }

        return team;
    }
}