using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Model.User;

[Table("Teammates")]
public class Teammate : IdentityUser        
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Nickname { get; set; }
    [Required]
    public ScrumRole ScrumRole { get; set; }
    [ForeignKey(nameof(Team))]
    public int TeamId { get; set; }
    
    public Team Team { get; set; }
    public ICollection<TeammateMeetings> TeammateMeetings { get; set; }
}