using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Context;
using DataAccess.Model.User;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class UserHierarchyStorage(ICreateAccessors factory, ILogger<UserHierarchyStorage> logger) : IUserHierarchyStorage
{
    public Team GetTeam(string teamGUID)
    {
        var teamAccessor = factory.Create<Team>();

        var team = teamAccessor.GetByPK(teamGUID);

        if (team is null)
        {
            logger.LogError($"Team with {teamGUID} does not exists");
            throw new AccessorException("User does not exists");        
        }
        
        return team;
    }

    public Project GetProjectByTeam(string teamGUID)
    {
        var dbContext = factory.DbContext;
        
        var project = dbContext
            .Projects
            .FirstOrDefault(p => p.TeamGUID == teamGUID);

        if (project is null)
        {
            logger.LogError($"Project with assigned Team {teamGUID} does not exists");
            throw new AccessorException($"Project with assigned Team {teamGUID} does not exists");        
        }

        return project;
    }

    public Team AddTeam(Team newTeam)
    {
        var teamAccessor = factory.Create<Team>();
        
        if (string.IsNullOrEmpty(newTeam.GUID))
            newTeam.GUID = Guid.NewGuid().ToString();

        var team =  teamAccessor.Add(newTeam);

        if (team is null)
        {
            logger.LogError("Team was not properly added");
            throw new AccessorException("Team was not properly added"); 
        }
     
        return newTeam;
    }

    public List<Team> GetAllTeams()
    {
        var teamAccessor = factory.Create<Team>();
        var teams = teamAccessor.GetAll();
        
        if (teams is null)
        {
            logger.LogError("Teams was not retrieved");
            throw new AccessorException("Teams was not retrieved"); 
        }

        return teams;
    }

    public Team GetTeamByGUID(string teamGUID)
    {
        var teamAccessor = factory.Create<Team>();
        
        var team = teamAccessor.GetByPK(teamGUID);
        if (team is null)
        {
            logger.LogError($"Team with {teamGUID} is not in the database");
            throw new AccessorException($"Team with {teamGUID} is not in the database"); 
        }

        return team;
    }
}