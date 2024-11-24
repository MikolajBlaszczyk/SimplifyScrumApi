using DataAccess.Models.Projects;
using UserModule.Security.Models;

namespace BacklogModule.Models.Results;

public class BacklogResult : BaseResult
{
    public BacklogResult() {}
    public BacklogResult(dynamic data) { Data = data; }
    public BacklogResult(Exception ex) : base(ex) { }
    
    public static BacklogResult SuccessWithoutData() => new();

    public static implicit operator BacklogResult(SprintRecord sprintInfo) => new(sprintInfo);
    public static implicit operator BacklogResult(ProjectRecord project) => new(project);
    public static implicit operator BacklogResult(FeatureRecord feature) => new(feature);
    public static implicit operator BacklogResult(TaskRecord task) => new(task);
    public static implicit operator BacklogResult(Exception ex) => new(ex);
}