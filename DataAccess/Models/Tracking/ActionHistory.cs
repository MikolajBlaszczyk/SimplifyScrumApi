using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Enums;
using DataAccess.Model.User;

namespace DataAccess.Models.Tracking;

[Table("ActionHistories")]
public class ActionHistory
{
    [Key]
    public long ID { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public HistoryAction Action { get; set; }
    [Required]
    public HistoryItem Item { get; set; }
    
    [Required]
    [StringLength(450, MinimumLength = 450)]
    public string UserGUID { get; set; }
    [Required]
    [StringLength(36, MinimumLength = 36)]
    public string ItemGUID { get; set; }

}