using BacklogModule.Abstraction;
using BacklogModule.Models;
using BacklogModule.Utils;
using DataAccess.Abstraction;

namespace BacklogModule;

public class BacklogManager(ISprintAccessor sprintAccessor) : IManageSprint
{
    public async Task<SprintRecord> GetSprintInfoForProject(string projectGUID)
    {
        var sprintModel =  sprintAccessor.GetCurrentSprintInfoByProject(projectGUID);

        if (sprintModel is null)
            throw new BacklogException("There is no sprint that belongs to this Project");

        return  ModelConverter.ConvertSprintModelToRecord(sprintModel);
    }
}