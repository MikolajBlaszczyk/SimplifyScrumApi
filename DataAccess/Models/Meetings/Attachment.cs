using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Enums;
using DataAccess.Model.ConnectionTables;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Model.Meetings;

[Table("Attachments")]
public class Attachment: IAccessorTable
{
    [Key]
    public int ID { get; set; }
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public  string Name { get; set; }
    [Required]
    [StringLength(100000)]
    public  string Content { get; set; }
    [Required]
    public  AttachmentType Type { get; set; }

    public ICollection<MeetingAttachments> MeetingAttachments { get; set; }
    public object GetPrimaryKey()
    {
        return ID;
    }
}