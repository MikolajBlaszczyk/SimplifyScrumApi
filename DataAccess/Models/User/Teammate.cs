using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using DataAccess.Models.Projects;
using Microsoft.AspNetCore.Identity;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Model.User;

[Table("Teammates")]
public class Teammate : IdentityUser, IAccessorTable
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Nickname { get; set; }
    public ScrumRole? ScrumRole { get; set; }
    
    [ForeignKey(nameof(Team))]
    public string? TeamGUID { get; set; }
    
    public Team Team { get; set; }
    public ICollection<TeammateMeetings> TeammateMeetings { get; set; }
    public ICollection<Task> Tasks { get; set; }
    public ICollection<SprintNote> SprintNotes { get; set; }
    public object GetPrimaryKey()
    {
        return Id;
    }
}