using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models.Projects;

[JsonSerializable(typeof(SprintRating))]
public class SprintRating
{
    public int Workflow { get; set; }
    public int Tools { get; set; }
    public int Communication { get; set; }
    public int ProblemResolving { get; set; }
    public int Pacing { get; set; }
}