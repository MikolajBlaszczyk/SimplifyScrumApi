using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Model.User;

namespace DataAccess.Models.Projects;

[Table("SprintNotes")]
public class SprintNote : ICloneable, IAccessorTable
{
    [Key]
    public int ID { get; set; }
    
    public SprintNoteValue Value { get; set; }
    
    [ForeignKey(nameof(Teammate))]
    [StringLength(450, MinimumLength = 450)]
    public string TeammateGUID { get; set; }
    [ForeignKey(nameof(Sprint))]
    public string SprintGUID { get; set; }
    

    public Teammate Teammate { get; set; }
    public Sprint Sprint { get; set; }
    public object Clone()
    {
        return new SprintNote
        {
            Value = Value,
            TeammateGUID = TeammateGUID,
            SprintGUID = SprintGUID,
        };
    }

    public object GetPrimaryKey()
    {
        return ID;
    }
}