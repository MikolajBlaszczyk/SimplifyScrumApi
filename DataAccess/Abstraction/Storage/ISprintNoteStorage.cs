using DataAccess.Models.Projects;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Abstraction.Storage;

public interface ISprintNoteStorage
{
    Task<SprintNote> AddSprintNotes(SprintNote note);
}