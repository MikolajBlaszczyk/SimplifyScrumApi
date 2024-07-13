using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;

namespace DataAccess.Model.ConnectionTables;

[Table("TeammateMeetings")]
public class TeammateMeetings
{
    public string TeammateGuid { get; set; }
    public Teammate Teammate { get; set; }

    public string MeetingGuid { get; set; }
    public Meeting Meeting { get; set; }
}