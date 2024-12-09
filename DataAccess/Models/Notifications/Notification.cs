using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Abstraction;
using DataAccess.Enums.Notification;

namespace DataAccess.Models.Notifications;

[Table("Notifications")]
public class Notification :  IAccessorTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }
    [Required]
    [StringLength(36, MinimumLength = 36)]
    
    public string NotificationSourceGUID { get; set; }
    [Required]
    public NotificationItem SourceType { get; set; }
    [Required]
    public NotificationType Type { get; set; }
    [Required]
    public int Advance { get; set; }
    [Required]
    public bool Sent { get; set; }

    public object GetPrimaryKey()
    {
        return ID;
    }
}