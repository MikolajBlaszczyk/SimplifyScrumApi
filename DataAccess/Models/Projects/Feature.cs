using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction.Tables;
using DataAccess.Enums;
using DataAccess.Model.User;

namespace DataAccess.Models.Projects;

[Table("Features")]
public class Feature : HistoryTable
{
    [Key]
    [StringLength(36, MinimumLength = 36)]
    public string GUID { get; set; }

    [StringLength(maximumLength:10000)]
    public string Description { get; set; }
    
    [Required]
    [StringLength(200, MinimumLength = 4)]
    public string Name { get; set; }
    [Required]
    public ExtendedStatus State { get; set; }
    public int? Points { get; set; }
    
    [ForeignKey(nameof(ParentProject))]
    [StringLength(36, MinimumLength = 36)]
    public string? ProjectGUID { get; set; }
    
    public Project ParentProject { get; set; }
    public ICollection<Task> Tasks { get; set; }
    
    public static bool operator ==(Feature first, Feature second)
    {
        return (
            first.GUID == second.GUID && 
            first.Name == second.Name && 
            first.ProjectGUID == second.ProjectGUID && 
            first.Description == second.Description && 
            first.Points == second.Points && 
            first.State == second.State
        );
    }

    public static bool operator !=(Feature first, Feature second)
    {
        return !(first == second);
    }
}