using DataAccess.Model.User;
using DataAccess.Models.Projects;
using UserModule.Security.Models;

namespace UserModule.Models;

public class HierarchyResult : BaseResult
{
    public HierarchyResult(Project project)
    {
        Project = project;
    }

    public HierarchyResult(Exception ex) : base(ex) { }
    
    public Project? Project { get; set; }
}