using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Models.Projects;

namespace DataAccess.Model.User;

[Table("Teams")]
public class Team: ICloneable, IAccessorTable
{
    [Key]
    [StringLength(36, MinimumLength = 36)]
    public string GUID { get; set; }
   
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Name { get; set; }
 
    
    [Required]
    public string ManagerGUID { get; set; }

    public ICollection<Teammate> TeamMembers { get; set; }
    public ICollection<Project> Projects { get; set; }
    public object Clone()
    {
        return new Team
        {
            GUID = Guid.NewGuid().ToString(),
            Name = Name,
            ManagerGUID = ManagerGUID
        };
    }

    public object GetPrimaryKey()
    {
        return GUID;
    }
}