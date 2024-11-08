using DataAccess.Models.Projects;

namespace UserModule.Models.Factories;

public class HierarchyResultFactory
{
 
    public static HierarchyResult Success(Project project)
    {
        return new HierarchyResult(project);
    }   
    public static HierarchyResult Failure(Exception ex)
    {
        return new HierarchyResult(ex);
    }
}