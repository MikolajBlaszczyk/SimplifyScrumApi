using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Tables;
using DataAccess.Model.ConnectionTables;

namespace DataAccess.Models.Projects;

[Table("Sprints")]
public class Sprint : HistoryTable, ICloneable, IAccessorTable
{
    [Key]
    [StringLength(36, MinimumLength = 36)]
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

    public bool IsFinished { get; set; }
    public Project Project { get; set; }
    public ICollection<SprintNote> SprintNotes { get; set; }
    public ICollection<SprintFeatures> SprintFeatures { get; set; }
    
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

    public object Clone()
    {
        return new Sprint
        {
            CreatedBy = CreatedBy,
            LastUpdatedBy = LastUpdatedBy,
            LastUpdateOn = LastUpdateOn,
            CreatedOn = CreatedOn,
            GUID = Guid.NewGuid().ToString(),
            Name = Name,
            Goal = Goal,
            Iteration = Iteration,
            End = End,
            ProjectGUID = ProjectGUID
        };
    }

    public object GetPrimaryKey()
    {
        return GUID;
    }
}