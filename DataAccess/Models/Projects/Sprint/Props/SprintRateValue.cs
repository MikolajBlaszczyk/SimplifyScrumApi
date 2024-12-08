using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models.Projects;

[JsonSerializable(typeof(SprintRateValue))]
public class SprintRateValue
{
    public List<Comment> Comments { get; set; }
    public SprintRating Rating { get; set; }
}