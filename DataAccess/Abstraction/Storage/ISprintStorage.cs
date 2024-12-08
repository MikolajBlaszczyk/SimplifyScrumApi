using DataAccess.Models.Projects;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Abstraction.Storage;

//TODO: Write tests
public interface ISprintStorage
{
    Sprint GetSprintInfoByProjectGUID(string projectGUID);
    Task<Sprint> GetSprintByGuid(string sprintGuid);
    Task<Sprint> AddSprint(Sprint sprint);
    Task<Sprint> UpdateSprint(Sprint sprint);
    Task LinkSprintWithFeatures(Sprint sprint, List<string> featureGuids);
}