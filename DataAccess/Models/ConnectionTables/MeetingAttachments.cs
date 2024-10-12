using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Model.Meetings;

namespace DataAccess.Model.ConnectionTables;

[Table("MeetingAttachments")]
public class MeetingAttachments
{
    [Required]
    [ForeignKey(nameof(Meeting))]
    [StringLength(36, MinimumLength = 36)]
    public string MeetingGUID { get; set; }
    public Meeting Meeting { get; set; }
    
    [Required]
    [ForeignKey(nameof(Attachment))]
    [StringLength(36, MinimumLength = 36)]
    public required int AttachmentID { get; set; }
    public Attachment Attachment { get; set; }
}