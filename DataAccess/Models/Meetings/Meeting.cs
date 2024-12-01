using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using DataAccess.Model.User;

namespace DataAccess.Model.Meetings;

[Table("Meetings")]
public class Meeting : ICloneable, IAccessorTable
{
    [Key]
    [StringLength(36, MinimumLength = 36)]
    public string GUID { get; set; }
    [Required]
    [StringLength(1000, MinimumLength = 2)]
    public string Name { get; set; }
    [StringLength(10000)]
    public string? Description { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public TimeSpan Duration { get; set; }
    [Required]
    public MeetingType Type { get; set; }
    [ForeignKey(nameof(MeetingLeader))]
    [StringLength(36, MinimumLength = 36)]
    public string MeetingLeaderGUID { get; set; }
    

    public Teammate MeetingLeader { get; set; }
    public ICollection<TeammateMeetings> TeammateMeetings { get; set; }
    public ICollection<MeetingAttachments> MeetingAttachments { get; set; }


    public object Clone()
    {
        return new Meeting
        {
            GUID = Guid.NewGuid().ToString(),
            Name = this.Name,
            Description = this.Description,
            Start = this.Start,
            Duration = this.Duration,
            Type = this.Type,
            MeetingLeaderGUID = this.MeetingLeaderGUID
        };
    }

    public object GetPrimaryKey()
    {
        return GUID;
    }
}