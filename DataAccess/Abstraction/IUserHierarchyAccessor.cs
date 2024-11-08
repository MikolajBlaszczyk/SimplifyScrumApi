using DataAccess.Model.User;
using DataAccess.Models.Projects;

namespace DataAccess.Abstraction;

public interface IUserHierarchyAccessor
{
    Project? GetProjectByTeam(string teamGUID);
}