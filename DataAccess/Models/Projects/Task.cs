using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Tables;
using DataAccess.Enums;
using DataAccess.Model.User;

namespace DataAccess.Models.Projects;

[Table("Tasks")]
public class Task : HistoryTable, ICloneable, IAccessorTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    
    [StringLength(1000)]
    public string Name { get; set; }
    [Required]
    public SimpleStatus State { get; set; }
    
    [ForeignKey(nameof(ParentFeature))]
    [Required]
    public string FeatureGUID { get; set; }
    [ForeignKey(nameof(AssignedTeammate))]
    public string? Assignee { get; set; }
    
    
    public Feature ParentFeature { get; set; }
    public Teammate AssignedTeammate { get; set; }
    
    public static bool operator ==(Task first, Task second)
    {
        return (
            first.ID == second.ID && 
            first.Name == second.Name && 
            first.FeatureGUID == second.FeatureGUID && 
            first.Assignee == second.Assignee && 
            first.State == second.State
        );
    }

    public static bool operator !=(Task first, Task second)
    {
        return !(first == second);
    }

    public object Clone()
    {
        return new Task
        {
            CreatedBy = CreatedBy,
            LastUpdatedBy = LastUpdatedBy,
            LastUpdateOn = LastUpdateOn,
            CreatedOn = CreatedOn,
            Name = Name,
            State = State,
            FeatureGUID = FeatureGUID,
            Assignee = Assignee,
        };
    }

    public object GetPrimaryKey()
    {
        return ID;
    }
}