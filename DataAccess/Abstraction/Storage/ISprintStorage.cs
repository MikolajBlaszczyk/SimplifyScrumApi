using DataAccess.Models.Projects;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Abstraction.Storage;

//TODO: Write tests
public interface ISprintStorage
{
    Sprint GetSprintInfoByProjectGUID(string projectGUID);
    
    Task<Sprint> AddSprint(Sprint sprint);
    Task LinkSprintWithFeatures(Sprint sprint, List<string> featureGuids);
}