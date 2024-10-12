using System.Text.Json.Serialization;

namespace DataAccess.Models.Projects;

[JsonSerializable(typeof(Assesment))]
public class Assesment
{
    public string Name { get; set; }
    public int Value { get; set; }
}