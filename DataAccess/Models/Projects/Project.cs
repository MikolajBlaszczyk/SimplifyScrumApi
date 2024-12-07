using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Tables;
using DataAccess.Enums;
using DataAccess.Model.User;

namespace DataAccess.Models.Projects;

[Table("Projects")]
public class Project : HistoryTable, ICloneable, IAccessorTable
{
    [Key]
    [StringLength(36, MinimumLength = 36)]
    public string GUID { get; set; }
    
    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string Name { get; set; }
    [StringLength(10000)]
    public string? Description { get; set; }
    [Required]
    public StandardStatus State { get; set; }
    
    
    [Required]
    [ForeignKey(nameof(ProjectTeam))]
    [StringLength(36, MinimumLength = 36)]
    public string TeamGUID { get; set; }

    public bool IsActive { get; set; }
    
    
    public Team ProjectTeam { get; set; }
    public ICollection<Feature> Features { get; set; }
    public ICollection<Sprint> Sprints { get; set; }
    
    public static bool operator ==(Project first, Project second)
    {
        return (
            first.GUID == second.GUID && 
            first.Name == second.Name && 
            first.TeamGUID == second.TeamGUID && 
            first.Description == second.Description && 
            first.State == second.State
        );
    }

    public static bool operator !=(Project first, Project second)
    {
        return !(first == second);
    }

    public object Clone()
    {
        return new Project
        {
            CreatedBy = CreatedBy,
            LastUpdatedBy = LastUpdatedBy,
            LastUpdateOn = LastUpdateOn,
            CreatedOn = CreatedOn,
            GUID = Guid.NewGuid().ToString(),
            Name = Name,
            Description = Description,
            State = State,
            TeamGUID = TeamGUID
        };
    }

    public object GetPrimaryKey()
    {
        return GUID;
    }
}