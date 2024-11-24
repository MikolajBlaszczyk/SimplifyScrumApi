using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction.Tables;

namespace DataAccess.Models.Projects;

[Table("Sprints")]
public class Sprint : HistoryTable
{
    [Key]
    public string GUID { get; set; }
    
    [StringLength(1000, MinimumLength = 3)]
    public string Name { get; set; }
    [StringLength(1000, MinimumLength = 3)]
    public string Goal { get; set; }
    public int Iteration { get; set; }
    public DateTime End { get; set; }
    
    [ForeignKey(nameof(Project))]
    [StringLength(36, MinimumLength = 36)]
    public string ProjectGUID { get; set; }
    
    public Project Project { get; set; }
    public ICollection<SprintNote> SprintNotes { get; set; }
    
    
    public static bool operator ==(Sprint first, Sprint second)
    {
        return (
            first.GUID == second.GUID && 
            first.Name == second.Name && 
            first.End == second.End && 
            first.Goal == second.Goal && 
            first.Iteration == second.Iteration && 
            first.ProjectGUID == second.ProjectGUID
        );
    }

    public static bool operator !=(Sprint first, Sprint second)
    {
        return !(first == second);
    }
}