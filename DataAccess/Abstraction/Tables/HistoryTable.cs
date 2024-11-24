using System.ComponentModel.DataAnnotations;

namespace DataAccess.Abstraction.Tables;

public class HistoryTable
{
    [Required]
    [StringLength(450, MinimumLength = 450)]
    //it is user id
    public string CreatedBy { get; set; }
    [Required]
    [StringLength(450, MinimumLength = 450)]
    //it is user id
    public string LastUpdatedBy { get; set; }

    [Required]
    public DateTime LastUpdateOn { get; set; }
    [Required]
    public DateTime CreatedOn { get; set; }
    
    public static bool operator ==(HistoryTable first, HistoryTable second)
    {
        return (
            first.CreatedBy == second.CreatedBy && 
            first.CreatedOn == second.CreatedOn && 
            first.LastUpdateOn == second.LastUpdateOn && 
            first.LastUpdatedBy == second.LastUpdatedBy
        );
    }

    public static bool operator !=(HistoryTable first, HistoryTable second)
    {
        return !(first == second);
    }
}