using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Storage;

public class SprintNoteStorage(ICreateAccessors factory, ILogger<SprintNoteStorage> logger) : ISprintNoteStorage
{
    private IAccessor<SprintNote> _accessor = factory.Create<SprintNote>();
    
    public async Task<SprintNote> AddSprintNotes(SprintNote note)
    {
        var addedSprintNote = await _accessor.Add(note);
        if (addedSprintNote is null)
        {
            logger.LogError("Cannot add sprint note");
            throw new AccessorException("Cannot add feature");
        }

        return addedSprintNote;
    }
}