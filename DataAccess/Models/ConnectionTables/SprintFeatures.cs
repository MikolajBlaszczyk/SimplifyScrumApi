using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Projects;

namespace DataAccess.Model.ConnectionTables;

[Table("SprintFeatures")]
public class SprintFeatures
{
    [Required]
    [ForeignKey(nameof(Sprint))]
    [StringLength(36, MinimumLength = 36)]
    public string SprintGUID { get; set; }
    public Sprint Sprint { get; set; }
    
    [Required]
    [ForeignKey(nameof(Feature))]
    [StringLength(36, MinimumLength = 36)]
    public string FeatureGUID { get; set; }
    public Feature Feature { get; set; }
    
}