using DataAccess.Model.User;
using DataAccess.Models.Projects;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Models;

public class HierarchyResult : BaseResult
{
    public HierarchyResult() {}
    public HierarchyResult(dynamic data) { Data = data; }
    public HierarchyResult(Exception ex) : base(ex) { }
    
    public static HierarchyResult SuccessWithoutData() => new();
    public static implicit operator HierarchyResult(Project project) => new(project);
    public static implicit operator HierarchyResult(List<SimpleTeamModel> teams) => new(teams);
    public static implicit operator HierarchyResult(List<SimpleUserModel> teams) => new(teams);
    public static implicit operator HierarchyResult(SimpleTeamModel team) => new(team);
    public static implicit operator HierarchyResult(Exception ex) => new(ex);
}