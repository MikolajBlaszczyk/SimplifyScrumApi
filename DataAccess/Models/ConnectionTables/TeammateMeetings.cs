using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;

namespace DataAccess.Model.ConnectionTables;

[Table("TeammateMeetings")]
public class TeammateMeetings
{
    [Required]
    [ForeignKey(nameof(Teammate))]
    [StringLength(450, MinimumLength = 450)]
    public string TeammateGUID { get; set; }
    public Teammate Teammate { get; set; }

    [Required]
    [ForeignKey(nameof(Meeting))]
    [StringLength(36, MinimumLength = 36)]
    public string MeetingGUID { get; set; }
    public Meeting Meeting { get; set; }
}