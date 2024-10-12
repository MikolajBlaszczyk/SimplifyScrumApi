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
    
    [ForeignKey(nameof(Project))]
    [StringLength(36, MinimumLength = 36)]
    public string ProjectGUID { get; set; }
    
    public Project Project { get; set; }
    public ICollection<SprintNote> SprintNotes { get; set; }
}