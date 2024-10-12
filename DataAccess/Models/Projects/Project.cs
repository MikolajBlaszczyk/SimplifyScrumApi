using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction.Tables;
using DataAccess.Enums;
using DataAccess.Model.User;

namespace DataAccess.Models.Projects;

[Table("Projects")]
public class Project : HistoryTable
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
    
    public Team ProjectTeam { get; set; }
    public ICollection<Feature> Features { get; set; }
    public ICollection<Sprint> Sprints { get; set; }
}