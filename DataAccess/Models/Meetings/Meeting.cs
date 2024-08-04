using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.User;

namespace DataAccess.Model.Meetings;

[Table("Meetings")]
public class Meeting
{
    [Key]
    public string Guid { get; set; }
    [Required]
    [StringLength(1000, MinimumLength = 2)]
    public string Name { get; set; }
    public string Description { get; set; }
    [ForeignKey(nameof(MeetingLeader))]
    public string MeetingLeaderGuid { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public TimeSpan Duration { get; set; }
    [Required]
    public MeetingType Type { get; set; }

    public Teammate MeetingLeader { get; set; }
    public ICollection<TeammateMeetings> TeammateMeetings { get; set; }
    public ICollection<MeetingAttachments> MeetingAttachments { get; set; }
    
    
}