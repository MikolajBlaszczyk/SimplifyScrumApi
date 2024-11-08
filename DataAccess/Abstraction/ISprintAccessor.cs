using DataAccess.Models.Projects;

namespace DataAccess.Abstraction;

public interface ISprintAccessor
{
    Sprint? GetCurrentSprintInfoByProject(string projectGUID);
    Sprint? GetSprintByGuid(string sprintGUID);
}