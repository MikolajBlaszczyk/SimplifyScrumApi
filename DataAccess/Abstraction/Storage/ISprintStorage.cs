using DataAccess.Models.Projects;

namespace DataAccess.Abstraction.Storage;

public interface ISprintStorage
{
    Sprint GetSprintInfoByProjectGUID(string projectGUID);

}