using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models.Projects;

[JsonSerializable(typeof(SprintNoteValue))]
[NotMapped]
public class SprintNoteValue
{
    public List<Note> Notes { get; set; }
    public SprintAssesment Assesment { get; set; }
}