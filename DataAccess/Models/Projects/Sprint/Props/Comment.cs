using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccess.Models.Projects;

[JsonSerializable(typeof(Comment))]
public class Comment
{
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteType Type { get; set; }
    public DateTime CreationDate { get; set; }
}