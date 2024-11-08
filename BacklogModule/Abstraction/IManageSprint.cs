using BacklogModule.Models;

namespace BacklogModule.Abstraction;

public interface IManageSprint
{
    Task<SprintRecord> GetSprintInfoForProject(string projectGUID);
}