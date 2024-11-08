using DataAccess.Models.Projects;

namespace DataAccess.Models.Factories;

public class SprintFactory
{
    public static Sprint CreateSprintWithGuid(string guid, string creator, string lastUpdate, string name, string goal, int iteration, DateTime end, string ProjectGUID)
    {
        return new Sprint
        {
            Creator = creator,
            LastUpdate = lastUpdate,
            GUID = guid,
            Name = name,
            Goal = goal,
            Iteration = iteration,
            End = end,
            ProjectGUID = ProjectGUID,
            Project = null,
            SprintNotes = null
        };
    }
}