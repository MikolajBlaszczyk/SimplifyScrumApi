using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model.User;

[Table("Teams")]
public class Team
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    public string ManagerGuid { get; set; }

    public ICollection<Teammate> TeamMembers { get; set; }
}