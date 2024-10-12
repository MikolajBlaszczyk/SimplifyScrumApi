using System.ComponentModel.DataAnnotations;

namespace DataAccess.Abstraction.Tables;

public class HistoryTable
{
    [Required]
    [StringLength(450, MinimumLength = 450)]
    public string Creator { get; set; }
    [Required]
    [StringLength(450, MinimumLength = 450)]
    public string LastUpdate { get; set; }
}