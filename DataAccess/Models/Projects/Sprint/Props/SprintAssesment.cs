using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models.Projects;

[JsonSerializable(typeof(SprintAssesment))]
[NotMapped]
public class SprintAssesment
{
    public Assesment Workflow { get; set; }
    public Assesment Tools { get; set; }
    public Assesment Communication { get; set; }
    public Assesment ProblemResolving { get; set; }
}