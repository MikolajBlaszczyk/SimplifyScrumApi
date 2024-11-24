using DataAccess.Models.Projects;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Abstraction;

public interface ISprintAccessor
{
    Sprint? GetSprintInfoByProjectGUID(string projectGUID);

}