using DataAccess.Model.User;
using DataAccess.Models.Projects;
using UserModule.Security.Models;

namespace UserModule.Models;

public class HierarchyResult : BaseResult
{
    public HierarchyResult(Project project) { Data = project; }
    public HierarchyResult(Exception ex) : base(ex) { }
    

    public static implicit operator HierarchyResult(Project project) => new(project);
    public static implicit operator HierarchyResult(Exception ex) => new(ex);
}