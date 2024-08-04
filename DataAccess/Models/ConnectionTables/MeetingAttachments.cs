using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Model.Meetings;

namespace DataAccess.Model.ConnectionTables;

[Table("MeetingAttachments")]
public class MeetingAttachments
{
    public string MeetingGuid { get; set; }
    public Meeting Meeting { get; set; }

    public int AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}